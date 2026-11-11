using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Implementation
{
    public sealed class PurchaseOrderLineRepository : ReadOnlyRepository<PurchaseOrderLine>, IPurchaseOrderLineRepository
    {
        public PurchaseOrderLineRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<PurchaseOrderLine?> GetByIdAsync(long lineId, CancellationToken cancellationToken = default)
        {
            return base.GetByIdAsync(new object[] { lineId }, cancellationToken);
        }
    }
}
