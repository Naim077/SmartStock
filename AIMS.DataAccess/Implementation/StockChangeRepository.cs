using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Implementation
{
    public sealed class StockChangeRepository : ReadOnlyRepository<StockChange>, IStockChangeRepository
    {
        public StockChangeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<StockChange?> GetByIdAsync(long documentId, CancellationToken cancellationToken = default)
        {
            return base.GetByIdAsync(new object[] { documentId }, cancellationToken);
        }
    }
}
