using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Implementation
{
    public sealed class GoodsReceiptReversalRepository : ReadOnlyRepository<GoodsReceiptReversal>, IGoodsReceiptReversalRepository
    {
        public GoodsReceiptReversalRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<GoodsReceiptReversal?> GetByIdAsync(long documentId, CancellationToken cancellationToken = default)
        {
            return base.GetByIdAsync(new object[] { documentId }, cancellationToken);
        }
    }
}
