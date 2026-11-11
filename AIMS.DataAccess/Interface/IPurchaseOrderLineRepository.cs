using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Interface
{
    public interface IPurchaseOrderLineRepository : IReadOnlyRepository<PurchaseOrderLine>
    {
        Task<PurchaseOrderLine?> GetByIdAsync(long lineId, CancellationToken cancellationToken = default);
    }
}
