using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Interface
{
    public interface IAiToolCallLogRepository
    {
        Task<List<AiToolCallLog>> GetAllAsync();
        Task<AiToolCallLog?> GetByIdAsync(long toolCallId);
        Task AddAsync(AiToolCallLog aiToolCallLog);
        Task UpdateAsync(AiToolCallLog aiToolCallLog);
        Task DeleteAsync(long toolCallId);
    }
}
