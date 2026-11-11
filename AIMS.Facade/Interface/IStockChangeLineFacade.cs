using AIMS.Entities;

namespace AIMS.Facade.Interface
{
    /// <summary>Read-only, detached queries for workflow-managed StockChangeLine records.</summary>
    /// <remarks>Independent mutations are not exposed; changes require the owning workflow.</remarks>
    public interface IStockChangeLineFacade
    {
        Task<List<StockChangeLine>> GetStockChangeLinesAsync(CancellationToken cancellationToken = default);
        Task<StockChangeLine?> GetStockChangeLineByIdAsync(long lineId, CancellationToken cancellationToken = default);
        Task<List<StockChangeLine>> GetStockChangeLinesPageAsync(int skip, int take, CancellationToken cancellationToken = default);
    }
}
