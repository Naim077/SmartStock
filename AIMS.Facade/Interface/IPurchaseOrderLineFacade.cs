using AIMS.Entities;

namespace AIMS.Facade.Interface
{
    /// <summary>Read-only, detached queries for workflow-managed PurchaseOrderLine records.</summary>
    /// <remarks>Independent mutations are not exposed; changes require the owning workflow.</remarks>
    public interface IPurchaseOrderLineFacade
    {
        Task<List<PurchaseOrderLine>> GetPurchaseOrderLinesAsync(CancellationToken cancellationToken = default);
        Task<PurchaseOrderLine?> GetPurchaseOrderLineByIdAsync(long lineId, CancellationToken cancellationToken = default);
        Task<List<PurchaseOrderLine>> GetPurchaseOrderLinesPageAsync(int skip, int take, CancellationToken cancellationToken = default);
    }
}
