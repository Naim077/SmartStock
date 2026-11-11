using AIMS.Entities;

namespace AIMS.Facade.Interface
{
    /// <summary>Read-only, detached queries for workflow-managed AuditLog records.</summary>
    /// <remarks>Independent mutations are not exposed; changes require the owning workflow.</remarks>
    public interface IAuditLogFacade
    {
        Task<List<AuditLog>> GetAuditLogsAsync(CancellationToken cancellationToken = default);
        Task<AuditLog?> GetAuditLogByIdAsync(long auditLogId, CancellationToken cancellationToken = default);
        Task<List<AuditLog>> GetAuditLogsPageAsync(int skip, int take, CancellationToken cancellationToken = default);
    }
}
