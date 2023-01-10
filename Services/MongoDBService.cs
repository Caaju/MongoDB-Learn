using CRUDMongo.Domain.Entities;
using CRUDMongo.Infra.Config;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CRUDMongoDB.Services
{
    public class MongoDBService
    {
        private readonly IMongoCollection<Playlist>? _playListCollection;

        public MongoDBService(IOptions<MongoDbSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase dataBase = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _playListCollection = dataBase.GetCollection<Playlist>(mongoDBSettings.Value.CollectionName);
        }

        public async Task<List<Playlist>> GetAsync()
        {
            return await _playListCollection.Find(
                new BsonDocument()).ToListAsync();
        }
        public async Task CreateAsync(Playlist playList)
        {
            await _playListCollection.InsertOneAsync(playList);
            await Task.CompletedTask;
        }
        public async Task AddToPlaylistAsync(string id, string movieId)
        {
            FilterDefinition<Playlist> filter = Builders<Playlist>
            .Filter.Eq("Id", id);

            UpdateDefinition<Playlist> update = Builders<Playlist>
            .Update
            .AddToSet<string>("items", movieId);

            await _playListCollection.UpdateOneAsync(filter, update);
            await Task.CompletedTask;
        }
        public async Task DeleteAsync(string id) 
        {
            FilterDefinition<Playlist> filter= Builders<Playlist>
            .Filter.Eq("Id",id);
            await _playListCollection.DeleteOneAsync(filter);
            await Task.CompletedTask;
         }
    }
}