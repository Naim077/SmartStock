using AIMS.DataAccess.Interface;
using AIMS.Entities;
using AIMS.Facade.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Implementation
{
    public class AuditLogFacade : IAuditLogFacade
    {
        private readonly IAuditLogRepository _auditLogRepository;

        public AuditLogFacade(IAuditLogRepository auditLogRepository)
        {
            _auditLogRepository = auditLogRepository;
        }

        public async Task<List<AuditLog>> GetAuditLogsAsync()
        {
            return await _auditLogRepository.GetAllAsync();
        }

        public async Task<AuditLog?> GetAuditLogByIdAsync(long auditLogId)
        {
            return await _auditLogRepository.GetByIdAsync(auditLogId);
        }

        public async Task AddAuditLogAsync(AuditLog auditLog)
        {
            ArgumentNullException.ThrowIfNull(auditLog);
            await _auditLogRepository.AddAsync(auditLog);
        }

        public async Task<bool> UpdateAuditLogAsync(AuditLog auditLog)
        {
            ArgumentNullException.ThrowIfNull(auditLog);
            var existing = await _auditLogRepository.GetByIdAsync(auditLog.AuditLogId);
            if (existing == null)
                return false;

            await _auditLogRepository.UpdateAsync(auditLog);
            return true;
        }

        public async Task<bool> DeleteAuditLogAsync(long auditLogId)
        {
            var existing = await _auditLogRepository.GetByIdAsync(auditLogId);
            if (existing == null)
                return false;

            await _auditLogRepository.DeleteAsync(auditLogId);
            return true;
        }
    }
}
