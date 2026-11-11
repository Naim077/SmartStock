using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Interface
{
    public interface IAiChatLogRepository : IReadOnlyRepository<AiChatLog>
    {
        Task<AiChatLog?> GetByIdAsync(long aiChatLogId, CancellationToken cancellationToken = default);
    }
}
