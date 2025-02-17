namespace YongQing.Repositories
{
    public interface IRepository<TEntity, TId> where TEntity : class
    {
        Task<List<TEntity>?> GetAllAsync();
        Task<TEntity?> GetByIdAsync(TId id);
        Task<int> CreateAsync(TEntity entity);
        Task<int> UpdateAsync(TEntity entity, TId id);
        Task<int> DeleteAsync(TId id);
    }
}
