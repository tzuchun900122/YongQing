namespace YongQing.Repositories
{
    public interface IRepository<TEntity, TId> where TEntity : class
    {
        Task<List<TEntity>?> GetAllAsync();
        Task<TEntity?> GetByIdAsync(TId id);
        Task<int> AddAsync(TEntity entity);
        Task<int> DeleteAsync(TId id);
        Task<int> SaveChangesAsync();
        Task<T> RunSafeAsync<T>(Func<Task<T>> action, T? fallbackValue = default!);
    }
}
