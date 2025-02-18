using YongQing.Entities;

namespace YongQing.Repositories
{
    public interface ICustomerRepository
    {
        Task<List<Customer>?> GetAllAsync();
        Task<Customer?> GetByIdAsync(String id);
        Task<int> AddAsync(Customer customer);
        Task<int> DeleteAsync(String id);
        Task<int> SaveChangesAsync();
        Task<T> RunSafeAsync<T>(Func<Task<T>> action, T? fallbackValue = default!);
    }
}
