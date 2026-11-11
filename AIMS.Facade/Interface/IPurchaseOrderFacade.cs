using AIMS.Entities;

namespace AIMS.Facade.Interface
{
    /// <summary>Read-only, detached queries for workflow-managed PurchaseOrder records.</summary>
    /// <remarks>Independent mutations are not exposed; changes require the owning workflow.</remarks>
    public interface IPurchaseOrderFacade
    {
        Task<List<PurchaseOrder>> GetPurchaseOrdersAsync(CancellationToken cancellationToken = default);
        Task<PurchaseOrder?> GetPurchaseOrderByIdAsync(long documentId, CancellationToken cancellationToken = default);
        Task<List<PurchaseOrder>> GetPurchaseOrdersPageAsync(int skip, int take, CancellationToken cancellationToken = default);
    }
}
