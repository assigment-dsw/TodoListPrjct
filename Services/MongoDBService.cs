using System;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TodoList.Models;

namespace TodoList.Services
{
	public class MongoDBService
	{
        private readonly IMongoCollection<TodoItem> _todoItemCollection;

        public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _todoItemCollection = database.GetCollection<TodoItem>(mongoDBSettings.Value.CollectionName);
        }

        public async Task<List<TodoItem>> GetAsync()
        {
            return await _todoItemCollection.Find(item => true).ToListAsync();
        }

        public async Task CreateAsync(TodoItem todoItem)
        {
            await _todoItemCollection.InsertOneAsync(todoItem);
        }

        public async Task UpdateAsync(string id, TodoItem todoItem)
        {
            var filter = Builders<TodoItem>.Filter.Eq(item => item.Id, id);
            await _todoItemCollection.ReplaceOneAsync(filter, todoItem);
        }

        public async Task DeleteAsync(string id)
        {
            var filter = Builders<TodoItem>.Filter.Eq(item => item.Id, id);
            await _todoItemCollection.DeleteOneAsync(filter);
        }

        public async Task<TodoItem> GetAsyncById(string id)
        {
            var filter = Builders<TodoItem>.Filter.Eq(item => item.Id, id);
            return await _todoItemCollection.Find(filter).FirstOrDefaultAsync();
        }

      
    }
}

