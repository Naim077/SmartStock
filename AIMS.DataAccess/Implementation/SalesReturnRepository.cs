using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Implementation
{
    public sealed class SalesReturnRepository : ReadOnlyRepository<SalesReturn>, ISalesReturnRepository
    {
        public SalesReturnRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<SalesReturn?> GetByIdAsync(long documentId, CancellationToken cancellationToken = default)
        {
            return base.GetByIdAsync(new object[] { documentId }, cancellationToken);
        }
    }
}
