
using GameDashboard.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDashboard.Application.Repositories
{
    public interface IWriteRepository<T> where T : IEntity, new() 
    {
        Task AddAsync(T entity);
        Task AddRangeAsync(IList<T> entities);
        Task<T> UpdateAsync(T entity);
        Task RemoveAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
