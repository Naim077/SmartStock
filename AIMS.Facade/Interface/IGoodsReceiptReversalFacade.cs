using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Interface
{
    public interface IGoodsReceiptReversalFacade
    {
        Task<List<GoodsReceiptReversal>> GetGoodsReceiptReversalsAsync();
        Task<GoodsReceiptReversal?> GetGoodsReceiptReversalByIdAsync(long documentId);
        Task AddGoodsReceiptReversalAsync(GoodsReceiptReversal goodsReceiptReversal);
        Task<bool> UpdateGoodsReceiptReversalAsync(GoodsReceiptReversal goodsReceiptReversal);
        Task<bool> DeleteGoodsReceiptReversalAsync(long documentId);
    }
}
