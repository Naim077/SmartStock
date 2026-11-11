using AIMS.Entities;

namespace AIMS.Facade.Interface
{
    /// <summary>Read-only, detached queries for workflow-managed StockChange records.</summary>
    /// <remarks>Independent mutations are not exposed; changes require the owning workflow.</remarks>
    public interface IStockChangeFacade
    {
        Task<List<StockChange>> GetStockChangesAsync(CancellationToken cancellationToken = default);
        Task<StockChange?> GetStockChangeByIdAsync(long documentId, CancellationToken cancellationToken = default);
        Task<List<StockChange>> GetStockChangesPageAsync(int skip, int take, CancellationToken cancellationToken = default);
    }
}
