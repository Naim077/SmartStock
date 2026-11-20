using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Interface
{
    public interface ICustomerFacade
    {
        Task<List<Customer>> GetCustomersAsync();
        Task<Customer?> GetCustomerByIdAsync(long customerId);
        Task AddCustomerAsync(Customer customer);
        Task<bool> UpdateCustomerAsync(Customer customer);
        Task<bool> DeleteCustomerAsync(long customerId);
    }
}
