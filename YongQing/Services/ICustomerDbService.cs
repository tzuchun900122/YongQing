using Microsoft.AspNetCore.JsonPatch;
using YongQing.Entities;

namespace YongQing.Services
{
    public interface ICustomerDbService
    {
        Task<List<Customer>?> GetAllAsync();
        Task<Customer?> GetByIdAsync(String id);
        Task<int> CreateAsync(Customer customer);
        Task<int> PatchAsync(String id, JsonPatchDocument<Customer> patchDoc);
        Task<int> DeleteAsync(String id);
    }
}
