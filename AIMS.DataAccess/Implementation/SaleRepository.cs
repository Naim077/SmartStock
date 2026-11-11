using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Implementation
{
    public sealed class SaleRepository : ReadOnlyRepository<Sale>, ISaleRepository
    {
        public SaleRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<Sale?> GetByIdAsync(long documentId, CancellationToken cancellationToken = default)
        {
            return base.GetByIdAsync(new object[] { documentId }, cancellationToken);
        }
    }
}
