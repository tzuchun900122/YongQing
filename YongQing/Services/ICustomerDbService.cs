using Microsoft.AspNetCore.JsonPatch;
using YongQing.Entities;

namespace YongQing.Services
{
    public interface ICustomerDbService
    {
        Task<List<Customer>?> GetAllAsync();
        Task<Customer?> GetByIdAsync(string id);
        Task<int> CreateAsync(Customer customer);
        Task<int> UpdateAsync(string id, Customer customer);
        Task<int> DeleteAsync(string id);
    }
}
