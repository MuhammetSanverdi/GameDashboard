using GameDashboard.Domain.Abstract;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDashboard.Application.Repositories
{
    public interface INoSQLRepository<T> where T:INoSQLEntity,new()
    {
        Task<IList<T>> GetAllAsync(FilterDefinition<T>? filter = null);

        Task<T> GetAsync(string id);

        Task CreateOneAsync(T entity);
        Task CreateManyAsync(IList<T> entities);

        Task UpdateAsync(T item);

        Task RemoveAsync(string id);
        Task DeleteAsync(T entity);
    }
}