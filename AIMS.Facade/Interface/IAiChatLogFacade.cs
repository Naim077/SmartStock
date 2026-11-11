using AIMS.Entities;

namespace AIMS.Facade.Interface
{
    /// <summary>Read-only, detached queries for workflow-managed AiChatLog records.</summary>
    /// <remarks>Independent mutations are not exposed; changes require the owning workflow.</remarks>
    public interface IAiChatLogFacade
    {
        Task<List<AiChatLog>> GetAiChatLogsAsync(CancellationToken cancellationToken = default);
        Task<AiChatLog?> GetAiChatLogByIdAsync(long aiChatLogId, CancellationToken cancellationToken = default);
        Task<List<AiChatLog>> GetAiChatLogsPageAsync(int skip, int take, CancellationToken cancellationToken = default);
    }
}
