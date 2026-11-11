using AIMS.Entities;

namespace AIMS.Facade.Interface
{
    /// <summary>Read-only, detached queries for workflow-managed SalesReturnLine records.</summary>
    /// <remarks>Independent mutations are not exposed; changes require the owning workflow.</remarks>
    public interface ISalesReturnLineFacade
    {
        Task<List<SalesReturnLine>> GetSalesReturnLinesAsync(CancellationToken cancellationToken = default);
        Task<SalesReturnLine?> GetSalesReturnLineByIdAsync(long lineId, CancellationToken cancellationToken = default);
        Task<List<SalesReturnLine>> GetSalesReturnLinesPageAsync(int skip, int take, CancellationToken cancellationToken = default);
    }
}
