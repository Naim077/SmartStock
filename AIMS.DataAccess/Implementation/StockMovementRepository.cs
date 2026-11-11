using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Implementation
{
    public sealed class StockMovementRepository : ReadOnlyRepository<StockMovement>, IStockMovementRepository
    {
        public StockMovementRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<StockMovement?> GetByIdAsync(long stockMovementId, CancellationToken cancellationToken = default)
        {
            return base.GetByIdAsync(new object[] { stockMovementId }, cancellationToken);
        }
    }
}
