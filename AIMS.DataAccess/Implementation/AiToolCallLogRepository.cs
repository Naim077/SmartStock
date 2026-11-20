using AIMS.DataAccess.Interface;
using AIMS.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Implementation
{
    public class AiToolCallLogRepository : IAiToolCallLogRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AiToolCallLogRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<AiToolCallLog>> GetAllAsync()
        {
            return await _dbContext.AiToolCallLogs.ToListAsync();
        }

        public async Task<AiToolCallLog?> GetByIdAsync(long toolCallId)
        {
            return await _dbContext.AiToolCallLogs.FindAsync(new object[] { toolCallId });
        }

        public async Task AddAsync(AiToolCallLog aiToolCallLog)
        {
            await _dbContext.AiToolCallLogs.AddAsync(aiToolCallLog);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(AiToolCallLog aiToolCallLog)
        {
            _dbContext.AiToolCallLogs.Update(aiToolCallLog);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(long toolCallId)
        {
            var aiToolCallLog = await _dbContext.AiToolCallLogs.FindAsync(new object[] { toolCallId });
            if (aiToolCallLog == null)
                return;

            _dbContext.AiToolCallLogs.Remove(aiToolCallLog);
            await _dbContext.SaveChangesAsync();
        }
    }
}
