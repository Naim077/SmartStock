using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Implementation
{
    public sealed class BusinessSettingsRepository : ReadOnlyRepository<BusinessSettings>, IBusinessSettingsRepository
    {
        public BusinessSettingsRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<BusinessSettings?> GetByIdAsync(byte settingsId, CancellationToken cancellationToken = default)
        {
            return base.GetByIdAsync(new object[] { settingsId }, cancellationToken);
        }
    }
}
