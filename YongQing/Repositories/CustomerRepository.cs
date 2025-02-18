using Microsoft.EntityFrameworkCore;
using YongQing.Entities;
using YongQing.Models;

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

        public async Task<ApiResult> GetAllAsync() =>
            await RunSafeAsync(() => _northwindDbContext.Customers.ToListAsync());    // 找不到回傳 new List<Customer>()

        public async Task<ApiResult> GetByIdAsync(String id) =>
            await RunSafeAsync(() => _northwindDbContext.Customers.FindAsync(id).AsTask());    // FindAsync 會回傳 ValueTask 所以必須 .AsTask()

        public async Task<ApiResult> UpdateAsync(string id, Customer customer)
        {
            return await RunSafeAsync(async () =>
            {
                var existingCustomer = await _northwindDbContext.Customers.FindAsync(id);
                if (existingCustomer is null) return 0;

                _northwindDbContext.Entry(existingCustomer).CurrentValues.SetValues(customer);
                return await _northwindDbContext.SaveChangesAsync();    // 回傳影響筆數
            }, -1);
        }

        public async Task<ApiResult> AddAsync(Customer customer)
        {
            return await RunSafeAsync(async () =>
            {
                if (customer is null) return 0;

                var existingCustomer = await _northwindDbContext.Customers.FindAsync(customer.CustomerID);    // 檢查是否存在
                if (existingCustomer is not null) return 0;

                await _northwindDbContext.Customers.AddAsync(customer);
                return await _northwindDbContext.SaveChangesAsync();
            }, -1);
        }

        public async Task<ApiResult> DeleteAsync(String id)
        {
            return await RunSafeAsync(async () =>
            {
                var existingCustomer = await _northwindDbContext.Customers.FindAsync(id);
                if (existingCustomer is null) return 0;

                _northwindDbContext.Remove(existingCustomer);
                return await _northwindDbContext.SaveChangesAsync();
            }, -1);
        }

        // 統一例外處理，出錯回傳設定值，方便控制層管理，並記錄錯誤訊息
        private async Task<ApiResult> RunSafeAsync<T>(Func<Task<T>> action, T? fallbackValue = default!)
        {
            try
            {
                var resultData = await action();
                return new ApiResult
                {
                    Data = resultData,
                    ErrorMessage = string.Empty
                };

            }
            catch (Exception ex)
            {
                var currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                _logger.LogError(ex, "Customer 資料庫錯誤發生於: {Time}", currentTime);
                return new ApiResult
                {
                    Data = fallbackValue,
                    ErrorMessage = ex.ToString()
                } ;
            }
        }
    }
}
