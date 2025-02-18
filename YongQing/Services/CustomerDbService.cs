using Microsoft.AspNetCore.JsonPatch;
using YongQing.Entities;
using YongQing.Repositories;

namespace YongQing.Services
{
    public class CustomerDbService : ICustomerDbService
    {
        private readonly ICustomerRepository _repository;
        public CustomerDbService(ICustomerRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<Customer>?> GetAllAsync() => await _repository.GetAllAsync();

        public async Task<Customer?> GetByIdAsync(string id)
            => await _repository.GetByIdAsync(id);

        public async Task<int> CreateAsync(Customer customer)
            => await _repository.AddAsync(customer);

        public async Task<int> UpdateAsync(string id, Customer customer)
            => await _repository.UpdateAsync(id, customer);

        public async Task<int> DeleteAsync(string id)
            => await _repository.DeleteAsync(id);
    }
}
