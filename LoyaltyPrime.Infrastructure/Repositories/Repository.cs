using EFCore.BulkExtensions;
using LoyaltyPrime.Domain.Common;
using LoyaltyPrime.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace LoyaltyPrime.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        protected readonly ApplicationDbContext _dbContext;

        private DbSet<T> _dbSet;

        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task CreateAsync(T entity)
        {
            entity.CreatedDateTime = DateTime.Now;
            await _dbSet.AddAsync(entity);
        }

        public async Task BulkInsertAsync(List<T> entities)
        {
            await _dbContext.BulkInsertAsync(entities, new BulkConfig { WithHoldlock = false });
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _dbContext.Database.BeginTransactionAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            await _dbContext.Database.RollbackTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            await _dbContext.Database.CommitTransactionAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
                _dbSet.Attach(entity);

            await Task.FromResult(_dbSet.Remove(entity));
        }

        public async Task DeleteByIdAsync(int id)
        {
            var entity = await GetAsync(id);
            await DeleteAsync(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> wheres)
        {
            return await _dbSet.Where(wheres).ToListAsync();
        }

        public IQueryable<T> GetQueryable()
        {
            return _dbSet.AsQueryable().AsNoTracking();
        }

        public async Task<T> GetAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);

            return entity;
        }

        public async Task<T> SingleAsync(Expression<Func<T, bool>> wheres)
        {
            var entity = await _dbSet.SingleOrDefaultAsync(wheres);

            return entity;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            await Task.FromResult(_dbSet.Update(entity));
        }

        private bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
