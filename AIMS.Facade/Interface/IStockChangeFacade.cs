using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Interface
{
    public interface IStockChangeFacade
    {
        Task<List<StockChange>> GetStockChangesAsync();
        Task<StockChange?> GetStockChangeByIdAsync(long documentId);
        Task AddStockChangeAsync(StockChange stockChange);
        Task<bool> UpdateStockChangeAsync(StockChange stockChange);
        Task<bool> DeleteStockChangeAsync(long documentId);
    }
}
