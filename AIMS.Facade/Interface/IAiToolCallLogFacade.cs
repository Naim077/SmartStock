using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Interface
{
    public interface IAiToolCallLogFacade
    {
        Task<List<AiToolCallLog>> GetAiToolCallLogsAsync();
        Task<AiToolCallLog?> GetAiToolCallLogByIdAsync(long toolCallId);
        Task AddAiToolCallLogAsync(AiToolCallLog aiToolCallLog);
        Task<bool> UpdateAiToolCallLogAsync(AiToolCallLog aiToolCallLog);
        Task<bool> DeleteAiToolCallLogAsync(long toolCallId);
    }
}
