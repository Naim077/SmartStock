using AIMS.Entities;
using AIMS.DataAccess.Interface;
using AIMS.Facade.Interface;

namespace AIMS.Facade.Implementation
{
    public sealed class AuditLogFacade : IAuditLogFacade
    {
        private readonly IAuditLogRepository _auditLogRepository;

        public AuditLogFacade(IAuditLogRepository auditLogRepository)
        {
            ArgumentNullException.ThrowIfNull(auditLogRepository);
            _auditLogRepository = auditLogRepository;
        }

        public Task<List<AuditLog>> GetAuditLogsAsync(CancellationToken cancellationToken = default)
        {
            return _auditLogRepository.GetAllAsync(cancellationToken);
        }

        public Task<AuditLog?> GetAuditLogByIdAsync(long auditLogId, CancellationToken cancellationToken = default)
        {
            return _auditLogRepository.GetByIdAsync(auditLogId, cancellationToken);
        }

        public Task<List<AuditLog>> GetAuditLogsPageAsync(int skip, int take, CancellationToken cancellationToken = default)
        {
            return _auditLogRepository.GetPageAsync(skip, take, cancellationToken);
        }
    }
}
