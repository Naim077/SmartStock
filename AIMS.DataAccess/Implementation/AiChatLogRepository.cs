using AIMS.DataAccess.Interface;
using AIMS.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Implementation
{
    public class AiChatLogRepository : IAiChatLogRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AiChatLogRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<AiChatLog>> GetAllAsync()
        {
            return await _dbContext.AiChatLogs.ToListAsync();
        }

        public async Task<AiChatLog?> GetByIdAsync(long aiChatLogId)
        {
            return await _dbContext.AiChatLogs.FindAsync(new object[] { aiChatLogId });
        }

        public async Task AddAsync(AiChatLog aiChatLog)
        {
            await _dbContext.AiChatLogs.AddAsync(aiChatLog);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(AiChatLog aiChatLog)
        {
            _dbContext.AiChatLogs.Update(aiChatLog);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(long aiChatLogId)
        {
            var aiChatLog = await _dbContext.AiChatLogs.FindAsync(new object[] { aiChatLogId });
            if (aiChatLog == null)
                return;

            _dbContext.AiChatLogs.Remove(aiChatLog);
            await _dbContext.SaveChangesAsync();
        }
    }
}
