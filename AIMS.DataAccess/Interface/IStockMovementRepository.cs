using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Interface
{
    public interface IStockMovementRepository
    {
        Task<List<StockMovement>> GetAllAsync();
        Task<StockMovement?> GetByIdAsync(long stockMovementId);
        Task AddAsync(StockMovement stockMovement);
        Task UpdateAsync(StockMovement stockMovement);
        Task DeleteAsync(long stockMovementId);
    }
}
