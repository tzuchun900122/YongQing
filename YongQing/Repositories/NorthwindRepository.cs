using YongQing.Entities;

namespace YongQing.Repositories
{
    public class NorthwindRepository<TEntity, TId> : Repository<TEntity, TId, NorthwindDbContext> where TEntity : class
    {
        public NorthwindRepository(NorthwindDbContext northwindDbContext) 
        : base(northwindDbContext)
        {
        }
    }
}
