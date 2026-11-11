using AIMS.Entities;

namespace AIMS.Facade.Interface
{
    /// <summary>Read-only, detached queries for workflow-managed SalesReturn records.</summary>
    /// <remarks>Independent mutations are not exposed; changes require the owning workflow.</remarks>
    public interface ISalesReturnFacade
    {
        Task<List<SalesReturn>> GetSalesReturnsAsync(CancellationToken cancellationToken = default);
        Task<SalesReturn?> GetSalesReturnByIdAsync(long documentId, CancellationToken cancellationToken = default);
        Task<List<SalesReturn>> GetSalesReturnsPageAsync(int skip, int take, CancellationToken cancellationToken = default);
    }
}
