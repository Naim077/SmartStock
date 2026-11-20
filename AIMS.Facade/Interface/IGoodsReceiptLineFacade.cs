using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Interface
{
    public interface IGoodsReceiptLineFacade
    {
        Task<List<GoodsReceiptLine>> GetGoodsReceiptLinesAsync();
        Task<GoodsReceiptLine?> GetGoodsReceiptLineByIdAsync(long lineId);
        Task AddGoodsReceiptLineAsync(GoodsReceiptLine goodsReceiptLine);
        Task<bool> UpdateGoodsReceiptLineAsync(GoodsReceiptLine goodsReceiptLine);
        Task<bool> DeleteGoodsReceiptLineAsync(long lineId);
    }
}
