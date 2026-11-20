using AIMS.DataAccess.Interface;
using AIMS.Entities;
using AIMS.Facade.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Implementation
{
    public class CustomerFacade : ICustomerFacade
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerFacade(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<List<Customer>> GetCustomersAsync()
        {
            return await _customerRepository.GetAllAsync();
        }

        public async Task<Customer?> GetCustomerByIdAsync(long customerId)
        {
            return await _customerRepository.GetByIdAsync(customerId);
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            ArgumentNullException.ThrowIfNull(customer);
            await _customerRepository.AddAsync(customer);
        }

        public async Task<bool> UpdateCustomerAsync(Customer customer)
        {
            ArgumentNullException.ThrowIfNull(customer);
            var existing = await _customerRepository.GetByIdAsync(customer.CustomerId);
            if (existing == null)
                return false;

            await _customerRepository.UpdateAsync(customer);
            return true;
        }

        public async Task<bool> DeleteCustomerAsync(long customerId)
        {
            var existing = await _customerRepository.GetByIdAsync(customerId);
            if (existing == null)
                return false;

            await _customerRepository.DeleteAsync(customerId);
            return true;
        }
    }
}
