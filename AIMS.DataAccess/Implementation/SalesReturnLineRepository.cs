using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Implementation
{
    public sealed class SalesReturnLineRepository : ReadOnlyRepository<SalesReturnLine>, ISalesReturnLineRepository
    {
        public SalesReturnLineRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<SalesReturnLine?> GetByIdAsync(long lineId, CancellationToken cancellationToken = default)
        {
            return base.GetByIdAsync(new object[] { lineId }, cancellationToken);
        }
    }
}
