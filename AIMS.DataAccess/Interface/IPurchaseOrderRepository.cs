using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Interface
{
    public interface IPurchaseOrderRepository : IReadOnlyRepository<PurchaseOrder>
    {
        Task<PurchaseOrder?> GetByIdAsync(long documentId, CancellationToken cancellationToken = default);
    }
}
