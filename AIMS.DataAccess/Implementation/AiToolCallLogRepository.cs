using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Implementation
{
    public sealed class AiToolCallLogRepository : ReadOnlyRepository<AiToolCallLog>, IAiToolCallLogRepository
    {
        public AiToolCallLogRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<AiToolCallLog?> GetByIdAsync(long toolCallId, CancellationToken cancellationToken = default)
        {
            return base.GetByIdAsync(new object[] { toolCallId }, cancellationToken);
        }
    }
}
