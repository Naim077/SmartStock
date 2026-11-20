using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Interface
{
    public interface IStockBalanceFacade
    {
        Task<List<StockBalance>> GetStockBalancesAsync();
        Task<StockBalance?> GetStockBalanceByIdAsync(long productBatchId, string bucket);
        Task AddStockBalanceAsync(StockBalance stockBalance);
        Task<bool> UpdateStockBalanceAsync(StockBalance stockBalance);
        Task<bool> DeleteStockBalanceAsync(long productBatchId, string bucket);
    }
}
