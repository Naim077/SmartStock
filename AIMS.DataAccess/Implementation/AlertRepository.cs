using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Implementation
{
    public sealed class AlertRepository : ReadOnlyRepository<Alert>, IAlertRepository
    {
        public AlertRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<Alert?> GetByIdAsync(long alertId, CancellationToken cancellationToken = default)
        {
            return base.GetByIdAsync(new object[] { alertId }, cancellationToken);
        }
    }
}
