using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Locally.Data;
using Locally.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Locally.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _usersCollection;

        static HttpClient client = new HttpClient();

        public UserService(
            IOptions<LocallyDatabaseSettings> LocallyDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                LocallyDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                LocallyDatabaseSettings.Value.DatabaseName);

            _usersCollection = mongoDatabase.GetCollection<User>(
                LocallyDatabaseSettings.Value.UsersCollectionName);
        }

        public async Task<List<User>> GetAsync() =>
            await _usersCollection.Find(_ => true).ToListAsync();

        public async Task<User?> GetAsync(string name) =>
            await _usersCollection.Find(x => x.Name == name).FirstOrDefaultAsync();

        public async Task CreateAsync(User newUser) =>
            await _usersCollection.InsertOneAsync(newUser);

        public async Task UpdateAsync(string name, User updatedUser) =>
            await _usersCollection.ReplaceOneAsync(x => x.Name == name, updatedUser);

        public async Task RemoveAsync(string id) =>
            await _usersCollection.DeleteOneAsync(x => x.Id == id);
    }
}