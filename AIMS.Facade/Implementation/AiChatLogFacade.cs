using AIMS.Entities;
using AIMS.DataAccess.Interface;
using AIMS.Facade.Interface;

namespace AIMS.Facade.Implementation
{
    public sealed class AiChatLogFacade : IAiChatLogFacade
    {
        private readonly IAiChatLogRepository _aiChatLogRepository;

        public AiChatLogFacade(IAiChatLogRepository aiChatLogRepository)
        {
            ArgumentNullException.ThrowIfNull(aiChatLogRepository);
            _aiChatLogRepository = aiChatLogRepository;
        }

        public Task<List<AiChatLog>> GetAiChatLogsAsync(CancellationToken cancellationToken = default)
        {
            return _aiChatLogRepository.GetAllAsync(cancellationToken);
        }

        public Task<AiChatLog?> GetAiChatLogByIdAsync(long aiChatLogId, CancellationToken cancellationToken = default)
        {
            return _aiChatLogRepository.GetByIdAsync(aiChatLogId, cancellationToken);
        }

        public Task<List<AiChatLog>> GetAiChatLogsPageAsync(int skip, int take, CancellationToken cancellationToken = default)
        {
            return _aiChatLogRepository.GetPageAsync(skip, take, cancellationToken);
        }
    }
}
