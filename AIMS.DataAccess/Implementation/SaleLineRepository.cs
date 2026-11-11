using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Implementation
{
    public sealed class SaleLineRepository : ReadOnlyRepository<SaleLine>, ISaleLineRepository
    {
        public SaleLineRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<SaleLine?> GetByIdAsync(long lineId, CancellationToken cancellationToken = default)
        {
            return base.GetByIdAsync(new object[] { lineId }, cancellationToken);
        }
    }
}
