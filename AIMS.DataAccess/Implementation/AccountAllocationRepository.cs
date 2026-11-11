using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Implementation
{
    public sealed class AccountAllocationRepository : ReadOnlyRepository<AccountAllocation>, IAccountAllocationRepository
    {
        public AccountAllocationRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<AccountAllocation?> GetByIdAsync(long allocationId, CancellationToken cancellationToken = default)
        {
            return base.GetByIdAsync(new object[] { allocationId }, cancellationToken);
        }
    }
}
