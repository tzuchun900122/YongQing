using Microsoft.AspNetCore.JsonPatch;

namespace YongQing.Services
{
    public interface IDbService<TEntity, TId> where TEntity : class
    {
        Task<List<TEntity>?> GetAllAsync();
        Task<TEntity?> GetByIdAsync(TId id);
        Task<int> CreateAsync(TEntity entity);
        Task<int> PatchAsync(TId id, JsonPatchDocument<TEntity> patchDoc);
        Task<int> DeleteAsync(TId id);
    }
}
