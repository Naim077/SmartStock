using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Interface
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer?> GetByIdAsync(long customerId, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(long customerId, byte[] rowVersion, CancellationToken cancellationToken = default);
    }
}
