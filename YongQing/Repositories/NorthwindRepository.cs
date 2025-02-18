using Microsoft.EntityFrameworkCore;
using YongQing.Entities;

namespace YongQing.Repositories
{
    public class NorthwindRepository<TEntity, TId> : BaseRepository<TEntity, TId> where TEntity : class
    {
        public NorthwindRepository(NorthwindDbContext NorthwindDbContext, ILogger<BaseRepository<TEntity, TId>> logger) 
        : base(NorthwindDbContext, logger)
        {
        }
    }
}
