using Microsoft.EntityFrameworkCore;

namespace YongQing.Repositories
{
    public class BaseRepository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : class
    {
        private readonly DbContext _dbContext;
        private readonly ILogger<BaseRepository<TEntity, TId>> _logger;
        protected BaseRepository(DbContext dbContext, ILogger<BaseRepository<TEntity, TId>> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public virtual async Task<List<TEntity>?> GetAllAsync() =>
            await RunSafeAsync(() => _dbContext.Set<TEntity>().ToListAsync());

        public virtual async Task<TEntity?> GetByIdAsync(TId id) =>
            await RunSafeAsync(() => _dbContext.Set<TEntity>().FindAsync(id).AsTask());

        public virtual async Task<int> AddAsync(TEntity entity)
        {
            return await RunSafeAsync(async () =>
            {
                  if (entity is null) return 0;

                await _dbContext.Set<TEntity>().AddAsync(entity);
                return await _dbContext.SaveChangesAsync();
            }, -1);
        }

        public virtual async Task<int> DeleteAsync(TId id)
        {
            return await RunSafeAsync(async () =>
            {
                var existingEntity = await _dbContext.Set<TEntity>().FindAsync(id);
                if (existingEntity is null) return 0;

                _dbContext.Remove(existingEntity);
                return await _dbContext.SaveChangesAsync();
            }, -1);
        }

        public virtual async Task<int> SaveChangesAsync() =>
            await RunSafeAsync(() => _dbContext.SaveChangesAsync());

        public virtual async Task<T> RunSafeAsync<T>(Func<Task<T>> action, T? fallbackValue = default!)
        {
            try
            {
                return await action();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error executing database operation in Repository<{Entity}>.", typeof(TEntity).Name);
                return fallbackValue!;
            }
        }
    }
}
