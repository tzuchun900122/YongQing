using YongQing.Entities;
using YongQing.Models;

namespace YongQing.Services
{
    public interface ICustomerDbService
    {
        Task<ApiResult> GetAllAsync();
        Task<ApiResult> GetByIdAsync(string id);
        Task<ApiResult> CreateAsync(Customer customer);
        Task<ApiResult> UpdateAsync(string id, Customer customer);
        Task<ApiResult> DeleteAsync(string id);
    }
}
