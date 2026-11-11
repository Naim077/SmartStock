using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Interface
{
    public interface IAuditLogRepository : IReadOnlyRepository<AuditLog>
    {
        Task<AuditLog?> GetByIdAsync(long auditLogId, CancellationToken cancellationToken = default);
    }
}
