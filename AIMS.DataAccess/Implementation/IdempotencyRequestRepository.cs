using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Implementation
{
    public sealed class IdempotencyRequestRepository : ReadOnlyRepository<IdempotencyRequest>, IIdempotencyRequestRepository
    {
        public IdempotencyRequestRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<IdempotencyRequest?> GetByIdAsync(long requestId, CancellationToken cancellationToken = default)
        {
            return base.GetByIdAsync(new object[] { requestId }, cancellationToken);
        }
    }
}
