using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Interface
{
    public interface IAiChatLogRepository
    {
        Task<List<AiChatLog>> GetAllAsync();
        Task<AiChatLog?> GetByIdAsync(long aiChatLogId);
        Task AddAsync(AiChatLog aiChatLog);
        Task UpdateAsync(AiChatLog aiChatLog);
        Task DeleteAsync(long aiChatLogId);
    }
}
