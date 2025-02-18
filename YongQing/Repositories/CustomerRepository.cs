using Microsoft.EntityFrameworkCore;
using YongQing.Entities;

namespace YongQing.Repositories
{
    // 用於 Northwind.dbo.Customers 的資料表
    public class CustomerRepository : ICustomerRepository
    {
        private readonly NorthwindDbContext _northwindDbContext;
        private readonly ILogger<CustomerRepository> _logger;

        public CustomerRepository(NorthwindDbContext northwindDbContext, ILogger<CustomerRepository> logger)
        {
            _northwindDbContext = northwindDbContext;
            _logger = logger;
        }

        public async Task<List<Customer>?> GetAllAsync() =>
            await RunSafeAsync(() => _northwindDbContext.Customers.ToListAsync());

        public async Task<Customer?> GetByIdAsync(String id) =>
            await RunSafeAsync(() => _northwindDbContext.Customers.FindAsync(id).AsTask());

        public async Task<int> UpdateAsync(string id, Customer customer)
        {
            return await RunSafeAsync(async () =>
            {
                var existingCustomer = await _northwindDbContext.Customers.FindAsync(id);
                if (existingCustomer is null) return 0;

                _northwindDbContext.Entry(existingCustomer).CurrentValues.SetValues(customer);
                return await _northwindDbContext.SaveChangesAsync();
            }, -1);
        }

        public async Task<int> AddAsync(Customer customer)
        {
            return await RunSafeAsync(async () =>
            {
                if (customer is null) return 0;

                await _northwindDbContext.Customers.AddAsync(customer);
                return await _northwindDbContext.SaveChangesAsync();
            }, -1);
        }

        public async Task<int> DeleteAsync(String id)
        {
            return await RunSafeAsync(async () =>
            {
                var existingCustomer = await _northwindDbContext.Customers.FindAsync(id);
                if (existingCustomer is null) return 0;

                _northwindDbContext.Remove(existingCustomer);
                return await _northwindDbContext.SaveChangesAsync();
            }, -1);
        }

        public async Task<T> RunSafeAsync<T>(Func<Task<T>> action, T? fallbackValue = default!)
        {
            try
            {
                return await action();
            }
            catch (Exception ex)
            {
                var currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                _logger.LogError(ex, "Customer 資料庫錯誤發生於: {Time}", currentTime);
                return fallbackValue!;
            }
        }


    }
}
