using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Interface
{
    public interface IStockChangeLineFacade
    {
        Task<List<StockChangeLine>> GetStockChangeLinesAsync();
        Task<StockChangeLine?> GetStockChangeLineByIdAsync(long lineId);
        Task AddStockChangeLineAsync(StockChangeLine stockChangeLine);
        Task<bool> UpdateStockChangeLineAsync(StockChangeLine stockChangeLine);
        Task<bool> DeleteStockChangeLineAsync(long lineId);
    }
}
