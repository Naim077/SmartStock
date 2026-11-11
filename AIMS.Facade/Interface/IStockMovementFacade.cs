using AIMS.Entities;

namespace AIMS.Facade.Interface
{
    /// <summary>Read-only, detached queries for workflow-managed StockMovement records.</summary>
    /// <remarks>Independent mutations are not exposed; changes require the owning workflow.</remarks>
    public interface IStockMovementFacade
    {
        Task<List<StockMovement>> GetStockMovementsAsync(CancellationToken cancellationToken = default);
        Task<StockMovement?> GetStockMovementByIdAsync(long stockMovementId, CancellationToken cancellationToken = default);
        Task<List<StockMovement>> GetStockMovementsPageAsync(int skip, int take, CancellationToken cancellationToken = default);
    }
}
