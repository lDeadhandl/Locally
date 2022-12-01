using System;
using System.Xml.Linq;
using Locally.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using static System.Net.WebRequestMethods;

namespace Locally.Services
{
	public class GamesService
	{
        private readonly IMongoCollection<Game> _gamesCollection;
        private JsonSerializerSettings _serializerSettings;
        private readonly UserService _userService;
        private readonly TeamsService _teamsService;
        private HttpClient _client;

        public GamesService(IOptions<LocallyDatabaseSettings> LocallyDatabaseSettings, UserService userService, TeamsService teamsService)
        {
            // Set the serializer settings to the snake case which is what the spotify responses are formatted as
            _serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                }
            };

            _client = new HttpClient();
            _userService = userService;
            _teamsService = teamsService;   


            var mongoClient = new MongoClient(
                LocallyDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                LocallyDatabaseSettings.Value.DatabaseName);

            _gamesCollection = mongoDatabase.GetCollection<Game>(
                LocallyDatabaseSettings.Value.GamesCollectionName);
        }
            
        public async Task<GamesObject> GetGames()
        {
            var gamesJson = await _client.GetStringAsync("http://api.sportradar.us/nba/trial/v7/en/games/2022/REG/schedule.json?api_key=3cdz4guhu3umeppcp8xf3wrr");
            var Games = JsonConvert.DeserializeObject<GamesObject>(gamesJson, _serializerSettings);

            //var easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            //Games?.Games?.ForEach(x => x.Scheduled = TimeZoneInfo.ConvertTimeFromUtc(x.Scheduled, easternZone));

            return Games;
        }

        public async Task<List<Game>> GetDailyGames(string name, string year, string month, string day)
        {
            var gamesJson = await _client.GetStringAsync($"http://api.sportradar.us/nba/trial/v7/en/games/{year}/{month}/{day}/schedule.json?api_key=3cdz4guhu3umeppcp8xf3wrr");
            var games = JsonConvert.DeserializeObject<GamesObject>(gamesJson, _serializerSettings).Games;

            var user = await _userService.GetAsync(name);

            var favoriteTeams = user.Favorites?.Teams;

            if (favoriteTeams == null)
            {
                return new List<Game>();
            }

            
            var tempGames = new List<Game>();

            foreach (var t in favoriteTeams)
            {
                // use favorite team id to check if it matches home/away team
                var game = games.FirstOrDefault(x => x.Away?.Id == t.Id || x.Home?.Id == t.Id);
                tempGames.Add(game);
            }

            var dashboardGames = tempGames.Distinct().ToList().Where(x => x != null && (x.Status != "inprogress" || x.Status != "closed")).ToList();
            var liveGames = new List<Game>();

            Thread.Sleep(1000);
            foreach (var game in dashboardGames)
            {
                if(game.Status != "scheduled")
                    continue;

                var liveGameJson = await _client.GetStringAsync($"http://api.sportradar.us/nba/trial/v7/en/games/6d2c3191-8604-4026-84d3-32f36d042a8e/boxscore.json?api_key=3cdz4guhu3umeppcp8xf3wrr");
                var liveGame = JsonConvert.DeserializeObject<Game>(liveGameJson, _serializerSettings);
                liveGames.Add(liveGame);
                Thread.Sleep(1000);
            }

            dashboardGames.AddRange(liveGames);
            //var easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            //Games?.Games?.ForEach(x => x.Scheduled = TimeZoneInfo.ConvertTimeFromUtc(x.Scheduled, easternZone));

            Thread.Sleep(1000);

            var allTeams = await _teamsService.GetTeams();
            foreach (var game in dashboardGames)
            {
                var homeTeam = allTeams.Find(x => x.Id == game.Home.Id);
                game.Home.Wins = homeTeam.Wins;
                game.Home.Losses = homeTeam.Losses;

                var awayTeam = allTeams.Find(x => x.Id == game.Away.Id);
                game.Away.Wins = homeTeam.Wins;
                game.Away.Losses = homeTeam.Losses;

            }

            return dashboardGames;
        }

        public async Task<List<Game>> GetAsync() =>
            await _gamesCollection.Find(_ => true).ToListAsync();

        //public async Task<Team?> GetAsync(string name) =>
        //    await _teamsCollection.Find(x => x.Name == name).FirstOrDefaultAsync();
        
        public async Task CreateAsync(GamesObject newGame) =>
            await _gamesCollection.InsertManyAsync(newGame.Games);
    }
}

