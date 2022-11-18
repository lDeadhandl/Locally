using System;
using Locally.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Locally.Services
{
    public class TeamsService
    {
        private JsonSerializerSettings _serializerSettings;
        private HttpClient _client;

        public TeamsService()
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

        }

        public async Task<ConferencesObject> GetConferences()
        {
            var conferencesJson = await _client.GetStringAsync("http://api.sportradar.us/nba/trial/v7/en/seasons/2022/REG/standings.json?api_key=3cdz4guhu3umeppcp8xf3wrr");
            var Conferences = JsonConvert.DeserializeObject<ConferencesObject>(conferencesJson, _serializerSettings);

            return Conferences;
        }
    }
}

