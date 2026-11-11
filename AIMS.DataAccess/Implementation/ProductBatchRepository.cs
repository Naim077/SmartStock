using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Implementation
{
    public sealed class ProductBatchRepository : ReadOnlyRepository<ProductBatch>, IProductBatchRepository
    {
        public ProductBatchRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<ProductBatch?> GetByIdAsync(long productBatchId, CancellationToken cancellationToken = default)
        {
            return base.GetByIdAsync(new object[] { productBatchId }, cancellationToken);
        }
    }
}
