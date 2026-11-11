using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Interface
{
    public interface IGoodsReceiptLineRepository : IReadOnlyRepository<GoodsReceiptLine>
    {
        Task<GoodsReceiptLine?> GetByIdAsync(long lineId, CancellationToken cancellationToken = default);
    }
}
