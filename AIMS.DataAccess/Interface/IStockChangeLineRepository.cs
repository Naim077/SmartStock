using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Interface
{
    public interface IStockChangeLineRepository
    {
        Task<List<StockChangeLine>> GetAllAsync();
        Task<StockChangeLine?> GetByIdAsync(long lineId);
        Task AddAsync(StockChangeLine stockChangeLine);
        Task UpdateAsync(StockChangeLine stockChangeLine);
        Task DeleteAsync(long lineId);
    }
}
