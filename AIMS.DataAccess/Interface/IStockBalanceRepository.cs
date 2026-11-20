using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Interface
{
    public interface IStockBalanceRepository
    {
        Task<List<StockBalance>> GetAllAsync();
        Task<StockBalance?> GetByIdAsync(long productBatchId, string bucket);
        Task AddAsync(StockBalance stockBalance);
        Task UpdateAsync(StockBalance stockBalance);
        Task DeleteAsync(long productBatchId, string bucket);
    }
}
