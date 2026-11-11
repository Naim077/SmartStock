using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Implementation
{
    public sealed class SupplierRepository : Repository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<Supplier?> GetByIdAsync(long supplierId, CancellationToken cancellationToken = default)
        {
            return base.GetByIdAsync(new object[] { supplierId }, cancellationToken);
        }

        public Task<bool> DeleteAsync(long supplierId, byte[] rowVersion, CancellationToken cancellationToken = default)
        {
            return base.DeleteAsync(new object[] { supplierId }, rowVersion, cancellationToken);
        }
    }
}
