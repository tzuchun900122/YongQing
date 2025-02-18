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

        public virtual async Task<Customer?> GetByIdAsync(String id) => await _repository.GetByIdAsync(id);

        public virtual async Task<int> CreateAsync(Customer customer) => await _repository.AddAsync(customer);

        public virtual async Task<int> PatchAsync(String id, JsonPatchDocument<Customer> patchDoc)
        {
            return await _repository.RunSafeAsync(async () =>
            {
                var existingCustomer = await _repository.GetByIdAsync(id);
                if (existingCustomer is null) return 0;

                patchDoc.ApplyTo(existingCustomer);

                return await _repository.SaveChangesAsync();
            }, -1);
        }

        public virtual async Task<int> DeleteAsync(String id) => await _repository.DeleteAsync(id);
    }
}
