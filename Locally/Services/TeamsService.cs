using System;
using Locally.Data;
using Locally.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Locally.Services
{
    public class TeamsService
    {
        private readonly IMongoCollection<Team> _teamsCollection;
        private JsonSerializerSettings _serializerSettings;
        private HttpClient _client;

        public TeamsService(IOptions<LocallyDatabaseSettings> LocallyDatabaseSettings)
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

            var mongoClient = new MongoClient(
            LocallyDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                LocallyDatabaseSettings.Value.DatabaseName);

            _teamsCollection = mongoDatabase.GetCollection<Team>(
                LocallyDatabaseSettings.Value.TeamsCollectionName);
        }

        public async Task<List<Team>> GetTeams()
        {
            var conferencesJson = await _client.GetStringAsync("http://api.sportradar.us/nba/trial/v7/en/seasons/2022/REG/standings.json?api_key=3cdz4guhu3umeppcp8xf3wrr");
            var Conferences = JsonConvert.DeserializeObject<ConferencesObject>(conferencesJson, _serializerSettings);

            var teams = new List<Team>();

            Conferences?.Conferences?
                .ForEach(x => x.Divisions?
                .ForEach(x => x.Teams?
                .ForEach(x => teams
                .Add(x))));

            return teams;
        }


        public async Task<List<Team>> GetAsync() =>
            await _teamsCollection.Find(_ => true).ToListAsync();

        public async Task<Team?> GetAsync(string name) =>
            await _teamsCollection.Find(x => x.Name == name).FirstOrDefaultAsync();

        public async Task CreateAsync(List<Team> newTeam) =>
            await _teamsCollection.InsertManyAsync(newTeam);


    }
}

