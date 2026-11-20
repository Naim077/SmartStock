using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Interface
{
    public interface IAiChatLogFacade
    {
        Task<List<AiChatLog>> GetAiChatLogsAsync();
        Task<AiChatLog?> GetAiChatLogByIdAsync(long aiChatLogId);
        Task AddAiChatLogAsync(AiChatLog aiChatLog);
        Task<bool> UpdateAiChatLogAsync(AiChatLog aiChatLog);
        Task<bool> DeleteAiChatLogAsync(long aiChatLogId);
    }
}
