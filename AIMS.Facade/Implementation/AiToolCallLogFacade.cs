using AIMS.Entities;
using AIMS.DataAccess.Interface;
using AIMS.Facade.Interface;

namespace AIMS.Facade.Implementation
{
    public sealed class AiToolCallLogFacade : IAiToolCallLogFacade
    {
        private readonly IAiToolCallLogRepository _aiToolCallLogRepository;

        public AiToolCallLogFacade(IAiToolCallLogRepository aiToolCallLogRepository)
        {
            ArgumentNullException.ThrowIfNull(aiToolCallLogRepository);
            _aiToolCallLogRepository = aiToolCallLogRepository;
        }

        public Task<List<AiToolCallLog>> GetAiToolCallLogsAsync(CancellationToken cancellationToken = default)
        {
            return _aiToolCallLogRepository.GetAllAsync(cancellationToken);
        }

        public Task<AiToolCallLog?> GetAiToolCallLogByIdAsync(long toolCallId, CancellationToken cancellationToken = default)
        {
            return _aiToolCallLogRepository.GetByIdAsync(toolCallId, cancellationToken);
        }

        public Task<List<AiToolCallLog>> GetAiToolCallLogsPageAsync(int skip, int take, CancellationToken cancellationToken = default)
        {
            return _aiToolCallLogRepository.GetPageAsync(skip, take, cancellationToken);
        }
    }
}
