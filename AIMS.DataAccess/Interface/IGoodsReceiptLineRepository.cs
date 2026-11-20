using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Interface
{
    public interface IGoodsReceiptLineRepository
    {
        Task<List<GoodsReceiptLine>> GetAllAsync();
        Task<GoodsReceiptLine?> GetByIdAsync(long lineId);
        Task AddAsync(GoodsReceiptLine goodsReceiptLine);
        Task UpdateAsync(GoodsReceiptLine goodsReceiptLine);
        Task DeleteAsync(long lineId);
    }
}
