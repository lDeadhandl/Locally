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

        public async Task<List<Game>> GetGames()
        {
            var gamesJson = await _client.GetStringAsync("http://api.sportradar.us/nba/trial/v7/en/games/2022/REG/schedule.json?api_key=3cdz4guhu3umeppcp8xf3wrr");
            var games = JsonConvert.DeserializeObject<GamesObject>(gamesJson, _serializerSettings).Games;
            var gamesWithESTSchedule = new List<Game>();

            foreach (var game in games)
            {
                // Converts from ISO 8601 -> EST
                game.Scheduled = DateTime.Parse(game.Scheduled, null, System.Globalization.DateTimeStyles.RoundtripKind).ToLocalTime().ToString();
            }

            return games;
        }

        public async Task<List<Game>> GetDailyGames(string name, string year, string month, string day)
        {
            var user = await _userService.GetAsync(name);
            var favoriteTeams = user.Favorites?.Teams;

            if (favoriteTeams == null)
                return new List<Game>();

            // Grabs games from DB instead of having to grab the daily schedule from the api 
            var schedule = await GetAsync();
            var games = schedule.FindAll(x => x.Scheduled.Contains($"{year}-{month}-{day}")); // day must contain leading 0

            var favoriteGames = new List<Game>();

            // Gets users favorite games from daily schedule
            foreach (var team in favoriteTeams)
            {
                var game = games.FirstOrDefault(x => x.Away?.Id == team.Id || x.Home?.Id == team.Id);
                favoriteGames.Add(game);
            }


            // Create list of schedule and upcoming games
            var upcomingGames = favoriteGames.Distinct().ToList().Where(x => x != null && x.Status == "scheduled").ToList();
            var liveGames = favoriteGames.Distinct().ToList().Where(x => x != null && x.Status == "inprogress").ToList();

            foreach (var game in liveGames)
            {
                var liveGameJson = await _client.GetStringAsync($"http://api.sportradar.us/nba/trial/v7/en/games/{game.Id}/boxscore.json?api_key=3cdz4guhu3umeppcp8xf3wrr");
                var liveGame = JsonConvert.DeserializeObject<Game>(liveGameJson, _serializerSettings);
                upcomingGames.Add(liveGame);
                Thread.Sleep(1000);
            }

            // Adds records to both teams in games
            var allTeams = await _teamsService.GetTeams();
            AddRecords(upcomingGames, allTeams);

            return upcomingGames;
        }

        public async Task<List<Game>> GetAsync() =>
            await _gamesCollection.Find(_ => true).ToListAsync();

        //public async Task<List<Game>> GetAsync(string date) =>
        //    await _gamesCollection.Find(x => x.Scheduled.Contains(date)).ToListAsync();

        public async Task CreateAsync(List<Game> newGames) =>
            await _gamesCollection.InsertManyAsync(newGames);

        private void AddRecords(List<Game> games, List<Team> teams) {

            foreach (var game in games)
            {
                var homeTeam = teams.Find(x => x.Id == game.Home.Id);
                game.Home.Wins = homeTeam.Wins;
                game.Home.Losses = homeTeam.Losses;

                var awayTeam = teams.Find(x => x.Id == game.Away.Id);
                game.Away.Wins = awayTeam.Wins;
                game.Away.Losses = awayTeam.Losses;

            }
        }

    }
}

