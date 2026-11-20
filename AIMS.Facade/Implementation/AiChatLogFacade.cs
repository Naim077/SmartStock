using AIMS.DataAccess.Interface;
using AIMS.Entities;
using AIMS.Facade.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Implementation
{
    public class AiChatLogFacade : IAiChatLogFacade
    {
        private readonly IAiChatLogRepository _aiChatLogRepository;

        public AiChatLogFacade(IAiChatLogRepository aiChatLogRepository)
        {
            _aiChatLogRepository = aiChatLogRepository;
        }

        public async Task<List<AiChatLog>> GetAiChatLogsAsync()
        {
            return await _aiChatLogRepository.GetAllAsync();
        }

        public async Task<AiChatLog?> GetAiChatLogByIdAsync(long aiChatLogId)
        {
            return await _aiChatLogRepository.GetByIdAsync(aiChatLogId);
        }

        public async Task AddAiChatLogAsync(AiChatLog aiChatLog)
        {
            ArgumentNullException.ThrowIfNull(aiChatLog);
            await _aiChatLogRepository.AddAsync(aiChatLog);
        }

        public async Task<bool> UpdateAiChatLogAsync(AiChatLog aiChatLog)
        {
            ArgumentNullException.ThrowIfNull(aiChatLog);
            var existing = await _aiChatLogRepository.GetByIdAsync(aiChatLog.AiChatLogId);
            if (existing == null)
                return false;

            await _aiChatLogRepository.UpdateAsync(aiChatLog);
            return true;
        }

        public async Task<bool> DeleteAiChatLogAsync(long aiChatLogId)
        {
            var existing = await _aiChatLogRepository.GetByIdAsync(aiChatLogId);
            if (existing == null)
                return false;

            await _aiChatLogRepository.DeleteAsync(aiChatLogId);
            return true;
        }
    }
}
