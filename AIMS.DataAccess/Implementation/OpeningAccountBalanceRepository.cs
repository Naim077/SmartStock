using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Implementation
{
    public sealed class OpeningAccountBalanceRepository : ReadOnlyRepository<OpeningAccountBalance>, IOpeningAccountBalanceRepository
    {
        public OpeningAccountBalanceRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<OpeningAccountBalance?> GetByIdAsync(long documentId, CancellationToken cancellationToken = default)
        {
            return base.GetByIdAsync(new object[] { documentId }, cancellationToken);
        }
    }
}
