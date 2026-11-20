using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Interface
{
    public interface IGoodsReceiptRepository
    {
        Task<List<GoodsReceipt>> GetAllAsync();
        Task<GoodsReceipt?> GetByIdAsync(long documentId);
        Task AddAsync(GoodsReceipt goodsReceipt);
        Task UpdateAsync(GoodsReceipt goodsReceipt);
        Task DeleteAsync(long documentId);
    }
}
