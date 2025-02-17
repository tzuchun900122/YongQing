using Microsoft.EntityFrameworkCore;

namespace YongQing.Entities
{
    public class NorthwindDbContext : DbContext
    {
        //初始化 NorthwindDbContext，並載入 DbContext 設定
        public NorthwindDbContext(DbContextOptions<NorthwindDbContext> options)
        : base(options)
        {
        }
    }
}
