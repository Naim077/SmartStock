using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Implementation
{
    public sealed class AuditLogRepository : ReadOnlyRepository<AuditLog>, IAuditLogRepository
    {
        public AuditLogRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<AuditLog?> GetByIdAsync(long auditLogId, CancellationToken cancellationToken = default)
        {
            return base.GetByIdAsync(new object[] { auditLogId }, cancellationToken);
        }
    }
}
