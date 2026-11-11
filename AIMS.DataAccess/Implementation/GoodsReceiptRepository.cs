using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Implementation
{
    public sealed class GoodsReceiptRepository : ReadOnlyRepository<GoodsReceipt>, IGoodsReceiptRepository
    {
        public GoodsReceiptRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<GoodsReceipt?> GetByIdAsync(long documentId, CancellationToken cancellationToken = default)
        {
            return base.GetByIdAsync(new object[] { documentId }, cancellationToken);
        }
    }
}
