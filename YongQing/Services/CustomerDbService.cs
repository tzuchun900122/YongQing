using YongQing.Entities;
using YongQing.Models;
using YongQing.Repositories;

namespace YongQing.Services
{
    // 用於操作 Northwind.dbo.Customers 資料表的 CRUD
    public class CustomerDbService : ICustomerDbService
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerDbService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<ApiResult> GetAllAsync()
            => await _customerRepository.GetAllAsync();

        public async Task<ApiResult> GetByIdAsync(string id)
            => await _customerRepository.GetByIdAsync(id);

        public async Task<ApiResult> CreateAsync(Customer customer)
            => await _customerRepository.AddAsync(customer);

        public async Task<ApiResult> UpdateAsync(string id, Customer customer)
            => await _customerRepository.UpdateAsync(id, customer);

        public async Task<ApiResult> DeleteAsync(string id)
            => await _customerRepository.DeleteAsync(id);
    }
}
