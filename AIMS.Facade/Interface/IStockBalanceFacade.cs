using AIMS.Entities;

namespace AIMS.Facade.Interface
{
    /// <summary>Read-only, detached queries for workflow-managed StockBalance records.</summary>
    /// <remarks>Independent mutations are not exposed; changes require the owning workflow.</remarks>
    public interface IStockBalanceFacade
    {
        Task<List<StockBalance>> GetStockBalancesAsync(CancellationToken cancellationToken = default);
        Task<StockBalance?> GetStockBalanceByIdAsync(long productBatchId, string bucket, CancellationToken cancellationToken = default);
        Task<List<StockBalance>> GetStockBalancesPageAsync(int skip, int take, CancellationToken cancellationToken = default);
    }
}
