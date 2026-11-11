using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Interface
{
    public interface IAiToolCallLogRepository : IReadOnlyRepository<AiToolCallLog>
    {
        Task<AiToolCallLog?> GetByIdAsync(long toolCallId, CancellationToken cancellationToken = default);
    }
}
