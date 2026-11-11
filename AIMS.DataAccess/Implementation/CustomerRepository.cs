using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Implementation
{
    public sealed class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<Customer?> GetByIdAsync(long customerId, CancellationToken cancellationToken = default)
        {
            return base.GetByIdAsync(new object[] { customerId }, cancellationToken);
        }

        public Task<bool> DeleteAsync(long customerId, byte[] rowVersion, CancellationToken cancellationToken = default)
        {
            return base.DeleteAsync(new object[] { customerId }, rowVersion, cancellationToken);
        }
    }
}
