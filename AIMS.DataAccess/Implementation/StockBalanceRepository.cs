using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Implementation
{
    public sealed class StockBalanceRepository : ReadOnlyRepository<StockBalance>, IStockBalanceRepository
    {
        public StockBalanceRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<StockBalance?> GetByIdAsync(long productBatchId, string bucket, CancellationToken cancellationToken = default)
        {
            return base.GetByIdAsync(new object[] { productBatchId, bucket }, cancellationToken);
        }
    }
}
