using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Interface
{
    public interface IAuditLogFacade
    {
        Task<List<AuditLog>> GetAuditLogsAsync();
        Task<AuditLog?> GetAuditLogByIdAsync(long auditLogId);
        Task AddAuditLogAsync(AuditLog auditLog);
        Task<bool> UpdateAuditLogAsync(AuditLog auditLog);
        Task<bool> DeleteAuditLogAsync(long auditLogId);
    }
}
