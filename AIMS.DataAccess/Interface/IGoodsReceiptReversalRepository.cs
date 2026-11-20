using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Interface
{
    public interface IGoodsReceiptReversalRepository
    {
        Task<List<GoodsReceiptReversal>> GetAllAsync();
        Task<GoodsReceiptReversal?> GetByIdAsync(long documentId);
        Task AddAsync(GoodsReceiptReversal goodsReceiptReversal);
        Task UpdateAsync(GoodsReceiptReversal goodsReceiptReversal);
        Task DeleteAsync(long documentId);
    }
}
