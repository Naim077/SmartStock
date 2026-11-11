using AIMS.Entities;

namespace AIMS.Facade.Interface
{
    /// <summary>Read-only, detached queries for workflow-managed Sale records.</summary>
    /// <remarks>Independent mutations are not exposed; changes require the owning workflow.</remarks>
    public interface ISaleFacade
    {
        Task<List<Sale>> GetSalesAsync(CancellationToken cancellationToken = default);
        Task<Sale?> GetSaleByIdAsync(long documentId, CancellationToken cancellationToken = default);
        Task<List<Sale>> GetSalesPageAsync(int skip, int take, CancellationToken cancellationToken = default);
    }
}
