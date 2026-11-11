using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Implementation
{
    public sealed class ApprovalRepository : ReadOnlyRepository<Approval>, IApprovalRepository
    {
        public ApprovalRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<Approval?> GetByIdAsync(long approvalId, CancellationToken cancellationToken = default)
        {
            return base.GetByIdAsync(new object[] { approvalId }, cancellationToken);
        }
    }
}
