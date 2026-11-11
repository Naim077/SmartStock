using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Implementation
{
    public sealed class StockChangeLineRepository : ReadOnlyRepository<StockChangeLine>, IStockChangeLineRepository
    {
        public StockChangeLineRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<StockChangeLine?> GetByIdAsync(long lineId, CancellationToken cancellationToken = default)
        {
            return base.GetByIdAsync(new object[] { lineId }, cancellationToken);
        }
    }
}
