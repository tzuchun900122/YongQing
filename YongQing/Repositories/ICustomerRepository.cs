using YongQing.Entities;

namespace YongQing.Repositories
{
    public interface ICustomerRepository
    {
        Task<List<Customer>?> GetAllAsync();
        Task<Customer?> GetByIdAsync(string id);
        Task<int> AddAsync(Customer customer);
        Task<int> UpdateAsync(string id, Customer customer);
        Task<int> DeleteAsync(string id);
    }
}
