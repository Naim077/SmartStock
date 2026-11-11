using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Implementation
{
    public sealed class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<Product?> GetByIdAsync(long productId, CancellationToken cancellationToken = default)
        {
            return base.GetByIdAsync(new object[] { productId }, cancellationToken);
        }

        public Task<bool> DeleteAsync(long productId, byte[] rowVersion, CancellationToken cancellationToken = default)
        {
            return base.DeleteAsync(new object[] { productId }, rowVersion, cancellationToken);
        }
    }
}
