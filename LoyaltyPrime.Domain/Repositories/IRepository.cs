using LoyaltyPrime.Domain.Common;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LoyaltyPrime.Domain.Repository
{
    public interface IRepository<T> where T : Entity
    {
        Task CreateAsync(T entity);

        Task BulkInsertAsync(List<T> entities);

        Task<IDbContextTransaction> BeginTransactionAsync();

        Task RollbackTransactionAsync();

        Task CommitTransactionAsync();

        Task DeleteByIdAsync(int id);

        Task DeleteAsync(T entity);

        Task UpdateAsync(T entity);

        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> wheres);

        IQueryable<T> GetQueryable();

        Task<T> GetAsync(int id);

        Task<T> SingleAsync(Expression<Func<T, bool>> wheres);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
