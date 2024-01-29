using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterApi.Infrastructure.Repositories.Base
{
    public interface IUnitOfWork : IDisposable
    {
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task CommitTransactionAsync(IDbContextTransaction transaction);
        void RollBackTransaction();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<int> SaveEntitiesAsync(CancellationToken cancellationToken = default);
    }
}
