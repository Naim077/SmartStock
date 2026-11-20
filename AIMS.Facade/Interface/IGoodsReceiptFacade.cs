using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Interface
{
    public interface IGoodsReceiptFacade
    {
        Task<List<GoodsReceipt>> GetGoodsReceiptsAsync();
        Task<GoodsReceipt?> GetGoodsReceiptByIdAsync(long documentId);
        Task AddGoodsReceiptAsync(GoodsReceipt goodsReceipt);
        Task<bool> UpdateGoodsReceiptAsync(GoodsReceipt goodsReceipt);
        Task<bool> DeleteGoodsReceiptAsync(long documentId);
    }
}
