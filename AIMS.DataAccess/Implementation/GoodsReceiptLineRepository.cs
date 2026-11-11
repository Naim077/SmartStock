using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Implementation
{
    public sealed class GoodsReceiptLineRepository : ReadOnlyRepository<GoodsReceiptLine>, IGoodsReceiptLineRepository
    {
        public GoodsReceiptLineRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<GoodsReceiptLine?> GetByIdAsync(long lineId, CancellationToken cancellationToken = default)
        {
            return base.GetByIdAsync(new object[] { lineId }, cancellationToken);
        }
    }
}
