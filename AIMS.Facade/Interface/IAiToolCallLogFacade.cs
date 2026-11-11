using AIMS.Entities;

namespace AIMS.Facade.Interface
{
    /// <summary>Read-only, detached queries for workflow-managed AiToolCallLog records.</summary>
    /// <remarks>Independent mutations are not exposed; changes require the owning workflow.</remarks>
    public interface IAiToolCallLogFacade
    {
        Task<List<AiToolCallLog>> GetAiToolCallLogsAsync(CancellationToken cancellationToken = default);
        Task<AiToolCallLog?> GetAiToolCallLogByIdAsync(long toolCallId, CancellationToken cancellationToken = default);
        Task<List<AiToolCallLog>> GetAiToolCallLogsPageAsync(int skip, int take, CancellationToken cancellationToken = default);
    }
}
