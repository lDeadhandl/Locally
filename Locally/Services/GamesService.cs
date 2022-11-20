using System;
using System.Xml.Linq;
using Locally.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Locally.Services
{
	public class GamesService
	{
        private readonly IMongoCollection<Game> _gamesCollection;
        private JsonSerializerSettings _serializerSettings;
        private readonly UserService _userService;
        private HttpClient _client;

        public GamesService(IOptions<LocallyDatabaseSettings> LocallyDatabaseSettings, UserService userService)
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

            var favoriteTeams = user.Favorites.Teams;
            var tempGames = new List<Game>();

            foreach (var t in favoriteTeams)
            {
                var awayGames = games.FirstOrDefault(x => x.Away.Id == t.Id);
                var homeGames = games.FirstOrDefault(x => x.Home.Id == t.Id);
                tempGames.Add(awayGames);
                tempGames.Add(homeGames);
            }

            var dashboardGames = tempGames.Distinct().ToList().Where(x => x != null).ToList();
            
            //var easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            //Games?.Games?.ForEach(x => x.Scheduled = TimeZoneInfo.ConvertTimeFromUtc(x.Scheduled, easternZone));

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

