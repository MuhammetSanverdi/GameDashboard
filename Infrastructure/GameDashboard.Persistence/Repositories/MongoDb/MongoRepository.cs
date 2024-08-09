using GameDashboard.Application.Repositories;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GameDashboard.Domain.Abstract;
using GameDashboard.Domain.Common;
using MongoDB.Driver;
using SharpCompress.Common;
using GameDashboard.Domain.Concrete;

namespace GameDashboard.Persistence.Repositories.MongoDb
{
    public class MongoRepository<T> : INoSQLRepository<T> where T : MongoEntity, new()
    {
            private readonly IMongoCollection<T> _items;

            public MongoRepository(IMongoDatabase database)
            {
                _items = database.GetCollection<T>($"{typeof(T).Name}s");
            }

            public async Task<IList<T>> GetAllAsync(FilterDefinition<T>? filter = null)
            {
                return await _items.Find(filter??Builders<T>.Filter.Empty).ToListAsync();

            }

            public async Task<T> GetAsync(string id)
            {
                return await _items.Find<T>(item => item.Id == id).FirstOrDefaultAsync();

            }

            public async Task CreateOneAsync(T entity)
            {
                entity.CreatedAt = DateTime.Now;
                entity.DataStatus = DataStatus.Created;
                await _items.InsertOneAsync(entity);

            }
        public async Task CreateManyAsync(IList<T> entities)
        {
            foreach (T entity in entities)
            {
                entity.CreatedAt = DateTime.Now;
                entity.DataStatus = DataStatus.Created;
            }
            await _items.InsertManyAsync(entities);

        }
        public async Task UpdateAsync(T item)
        {
            item.DataStatus = DataStatus.Updated;
            item.UpdatedAt = DateTime.Now;

            await _items.ReplaceOneAsync(item => item.Id == item.Id, item);

        }
            public async Task RemoveAsync(string id)
            {
                await _items.DeleteOneAsync(item => item.Id == id);
            }

        public async Task DeleteAsync(T entity)
        {
            var filter = Builders<T>.Filter.Eq(e => e.Id, entity.Id);
            var update = Builders<T>.Update.Set(e => e.DataStatus, DataStatus.Deleted)
                .Set(e=>e.DeletedAt,DateTime.Now);
            await _items.UpdateOneAsync(filter,update);
        }
       
    }
}
