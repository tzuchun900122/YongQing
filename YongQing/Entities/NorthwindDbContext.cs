using Microsoft.EntityFrameworkCore;

namespace YongQing.Entities
{
    // 用於 Northwind 資料庫
    public class NorthwindDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        //初始化資料庫上下文
        public NorthwindDbContext(DbContextOptions<NorthwindDbContext> options)
        : base(options)
        {
        }
    }
}
