
using Microsoft.AspNetCore.JsonPatch;
using YongQing.Repositories;

namespace YongQing.Services
{
    public class BaseDbService<TEntity, TId> : IDbService<TEntity, TId> where TEntity : class
    {
        private readonly IRepository<TEntity, TId> _repository;
        protected BaseDbService(IRepository<TEntity, TId> repository)
        {
            _repository = repository;
        }
        public async Task<List<TEntity>?> GetAllAsync() => await _repository.GetAllAsync();

        public virtual async Task<TEntity?> GetByIdAsync(TId id) => await _repository.GetByIdAsync(id);

        public virtual async Task<int> CreateAsync(TEntity entity) => await _repository.AddAsync(entity);

        public virtual async Task<int> PatchAsync(TId id, JsonPatchDocument<TEntity> patchDoc)
        {
            return await _repository.RunSafeAsync(async () =>
            {
                var existingEntity = await _repository.GetByIdAsync(id);
                if (existingEntity is null) return 0;

                patchDoc.ApplyTo(existingEntity);

                return await _repository.SaveChangesAsync();
            }, -1);

        }
        public virtual async Task<int> DeleteAsync(TId id) => await _repository.DeleteAsync(id);
    }
}
