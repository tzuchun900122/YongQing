using Microsoft.EntityFrameworkCore;

namespace YongQing.Entities
{
    public class NorthwindDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Customer> Orders { get; set; }
         public DbSet<Customer> Products { get; set; }
        //初始化 NorthwindDbContext，並載入 DbContext 設定
        public NorthwindDbContext(DbContextOptions<NorthwindDbContext> options)
        : base(options)
        {
        }
    }
}
