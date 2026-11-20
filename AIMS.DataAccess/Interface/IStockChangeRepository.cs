using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Interface
{
    public interface IStockChangeRepository
    {
        Task<List<StockChange>> GetAllAsync();
        Task<StockChange?> GetByIdAsync(long documentId);
        Task AddAsync(StockChange stockChange);
        Task UpdateAsync(StockChange stockChange);
        Task DeleteAsync(long documentId);
    }
}
