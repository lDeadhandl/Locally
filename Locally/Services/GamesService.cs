﻿using System;
using Locally.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Locally.Services
{
	public class GamesService
	{
        private JsonSerializerSettings _serializerSettings;
        private HttpClient _client;

        public GamesService()
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

        public async Task<GamesObject> GetGames()
        {
            var gamesJson = await _client.GetStringAsync("http://api.sportradar.us/nba/trial/v7/en/games/2022/REG/schedule.json?api_key=3cdz4guhu3umeppcp8xf3wrr");
            var Games = JsonConvert.DeserializeObject<GamesObject>(gamesJson, _serializerSettings);
           
            return Games;
        }
    }
}

