using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Implementation
{
    public sealed class AccountEntryRepository : ReadOnlyRepository<AccountEntry>, IAccountEntryRepository
    {
        public AccountEntryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<AccountEntry?> GetByIdAsync(long accountEntryId, CancellationToken cancellationToken = default)
        {
            return base.GetByIdAsync(new object[] { accountEntryId }, cancellationToken);
        }
    }
}
