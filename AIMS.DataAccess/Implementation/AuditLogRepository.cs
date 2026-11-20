using AIMS.DataAccess.Interface;
using AIMS.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Implementation
{
    public class AuditLogRepository : IAuditLogRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AuditLogRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<AuditLog>> GetAllAsync()
        {
            return await _dbContext.AuditLogs.ToListAsync();
        }

        public async Task<AuditLog?> GetByIdAsync(long auditLogId)
        {
            return await _dbContext.AuditLogs.FindAsync(new object[] { auditLogId });
        }

        public async Task AddAsync(AuditLog auditLog)
        {
            await _dbContext.AuditLogs.AddAsync(auditLog);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(AuditLog auditLog)
        {
            _dbContext.AuditLogs.Update(auditLog);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(long auditLogId)
        {
            var auditLog = await _dbContext.AuditLogs.FindAsync(new object[] { auditLogId });
            if (auditLog == null)
                return;

            _dbContext.AuditLogs.Remove(auditLog);
            await _dbContext.SaveChangesAsync();
        }
    }
}
