using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Interface
{
    public interface IGoodsReceiptRepository : IReadOnlyRepository<GoodsReceipt>
    {
        Task<GoodsReceipt?> GetByIdAsync(long documentId, CancellationToken cancellationToken = default);
    }
}
