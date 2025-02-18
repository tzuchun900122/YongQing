using YongQing.Repositories;

namespace YongQing.Services
{
    public class NorthwindDbService<TEntity, TId> : BaseDbService<TEntity, TId> where TEntity : class
    {
        protected NorthwindDbService(IRepository<TEntity, TId> NorthwindRepository)
        : base(NorthwindRepository)
        {
        }
    }
}
