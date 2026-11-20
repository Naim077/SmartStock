using AIMS.DataAccess.Interface;
using AIMS.Entities;
using AIMS.Facade.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Implementation
{
    public class AiToolCallLogFacade : IAiToolCallLogFacade
    {
        private readonly IAiToolCallLogRepository _aiToolCallLogRepository;

        public AiToolCallLogFacade(IAiToolCallLogRepository aiToolCallLogRepository)
        {
            _aiToolCallLogRepository = aiToolCallLogRepository;
        }

        public async Task<List<AiToolCallLog>> GetAiToolCallLogsAsync()
        {
            return await _aiToolCallLogRepository.GetAllAsync();
        }

        public async Task<AiToolCallLog?> GetAiToolCallLogByIdAsync(long toolCallId)
        {
            return await _aiToolCallLogRepository.GetByIdAsync(toolCallId);
        }

        public async Task AddAiToolCallLogAsync(AiToolCallLog aiToolCallLog)
        {
            ArgumentNullException.ThrowIfNull(aiToolCallLog);
            await _aiToolCallLogRepository.AddAsync(aiToolCallLog);
        }

        public async Task<bool> UpdateAiToolCallLogAsync(AiToolCallLog aiToolCallLog)
        {
            ArgumentNullException.ThrowIfNull(aiToolCallLog);
            var existing = await _aiToolCallLogRepository.GetByIdAsync(aiToolCallLog.ToolCallId);
            if (existing == null)
                return false;

            await _aiToolCallLogRepository.UpdateAsync(aiToolCallLog);
            return true;
        }

        public async Task<bool> DeleteAiToolCallLogAsync(long toolCallId)
        {
            var existing = await _aiToolCallLogRepository.GetByIdAsync(toolCallId);
            if (existing == null)
                return false;

            await _aiToolCallLogRepository.DeleteAsync(toolCallId);
            return true;
        }
    }
}
