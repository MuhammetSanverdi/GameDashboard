
using GameDashboard.Application.Repositories;
using GameDashboard.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IkProject.Persistence.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : SQLEntity, new()
    {
        private readonly DbContext _dbContext;

        public WriteRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private DbSet<T> Table { get => _dbContext.Set<T>(); }

        public async Task AddAsync(T entity)
        {
            entity.Id = Guid.NewGuid();
            entity.DataStatus = DataStatus.Created;
            entity.CreatedAt = DateTime.UtcNow;
            await Table.AddAsync(entity);
        }

        public async Task AddRangeAsync(IList<T> entities)
        {
            await Table.AddRangeAsync(entities);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            entity.DataStatus = DataStatus.Updated;
            entity.UpdatedAt = DateTime.UtcNow;
            await Task.Run(() => Table.Update(entity));
            return entity;
        }

        public async Task RemoveAsync(T entity)
        {
            await Task.Run(() => Table.Remove(entity));
        }

        public async Task DeleteAsync(T entity)
        {
            entity.DataStatus = DataStatus.Deleted;
            entity.DeletedAt = DateTime.UtcNow;
            await Task.Run(() => Table.Update(entity));
        }
    }
}
