using YongQing.Entities;
using YongQing.Models;

namespace YongQing.Repositories
{
    public interface ICustomerRepository
    {
        Task<ApiResult> GetAllAsync();
        Task<ApiResult> GetByIdAsync(string id);
        Task<ApiResult> AddAsync(Customer customer);
        Task<ApiResult> UpdateAsync(string id, Customer customer);
        Task<ApiResult> DeleteAsync(string id);
    }
}
