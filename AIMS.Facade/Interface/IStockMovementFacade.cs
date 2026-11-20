using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Interface
{
    public interface IStockMovementFacade
    {
        Task<List<StockMovement>> GetStockMovementsAsync();
        Task<StockMovement?> GetStockMovementByIdAsync(long stockMovementId);
        Task AddStockMovementAsync(StockMovement stockMovement);
        Task<bool> UpdateStockMovementAsync(StockMovement stockMovement);
        Task<bool> DeleteStockMovementAsync(long stockMovementId);
    }
}
