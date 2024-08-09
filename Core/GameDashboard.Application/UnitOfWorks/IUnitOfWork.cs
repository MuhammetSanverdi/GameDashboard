
using GameDashboard.Application.Repositories;
using GameDashboard.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDashboard.Application.UnitOfWorks
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IReadRepository<T> GetReadRepository<T>() where T : SQLEntity, new(); 
        IWriteRepository<T> GetWriteRepository<T>() where T : SQLEntity, new(); 
        Task<int> SaveAsync();
        int Save();
    }
}
