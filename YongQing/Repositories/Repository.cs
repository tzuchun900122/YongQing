using Microsoft.EntityFrameworkCore;

namespace YongQing.Repositories
{
    public class Repository<TEntity, TId, TContext> : IRepository<TEntity, TId> where TEntity : class where TContext : DbContext
    {
        private readonly TContext _dbContext;
        protected Repository(TContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<TEntity>?> GetAllAsync()
        {
            return await RunSafeAsync(async () =>
            {
                return await _dbContext.Set<TEntity>().ToListAsync();
            }, (List<TEntity>?)null);
        }

        public async Task<TEntity?> GetByIdAsync(TId id)
        {
            return await RunSafeAsync(async () =>
            {
                return await _dbContext.Set<TEntity>().FindAsync(id);
            }, null);
        }

        public async Task<int> CreateAsync(TEntity entity)
        {
            return await RunSafeAsync(async () =>
            {
                await _dbContext.Set<TEntity>().AddAsync(entity);
                return await _dbContext.SaveChangesAsync();
            }, -1);
        }

        public async Task<int> UpdateAsync(TEntity entity, TId id)
        {
            return await RunSafeAsync(async () =>
            {
                var existingEntity = await GetByIdAsync(id);
                if (existingEntity != null)
                {
                    _dbContext.Entry(existingEntity).CurrentValues.SetValues(entity);
                    return await _dbContext.SaveChangesAsync();
                }
                return 0;
            }, -1);
        }

        public async Task<int> DeleteAsync(TId id)
        {
            return await RunSafeAsync(async () =>
            {
                var existingEntity = await GetByIdAsync(id);
                if (existingEntity != null)
                {
                    _dbContext.Remove(existingEntity);
                    return await _dbContext.SaveChangesAsync();
                }
                return 0;
            }, -1);
        }
        private async Task<T> RunSafeAsync<T>(Func<Task<T>> action,T fallbackValue)
        {
            try
            {
                return await action();
            }
            catch
            {
                return fallbackValue;
            }
        }
    }
}
