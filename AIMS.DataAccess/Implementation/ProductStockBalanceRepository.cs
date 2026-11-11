using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Implementation
{
    public sealed class ProductStockBalanceRepository : ReadOnlyRepository<ProductStockBalance>, IProductStockBalanceRepository
    {
        public ProductStockBalanceRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<ProductStockBalance?> GetByIdAsync(long productId, CancellationToken cancellationToken = default)
        {
            return base.GetByIdAsync(new object[] { productId }, cancellationToken);
        }
    }
}
