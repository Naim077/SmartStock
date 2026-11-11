using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Implementation
{
    public sealed class AiChatLogRepository : ReadOnlyRepository<AiChatLog>, IAiChatLogRepository
    {
        public AiChatLogRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<AiChatLog?> GetByIdAsync(long aiChatLogId, CancellationToken cancellationToken = default)
        {
            return base.GetByIdAsync(new object[] { aiChatLogId }, cancellationToken);
        }
    }
}
