using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Implementation
{
    public sealed class ForecastPointRepository : ReadOnlyRepository<ForecastPoint>, IForecastPointRepository
    {
        public ForecastPointRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<ForecastPoint?> GetByIdAsync(long forecastRunId, short horizonDays, CancellationToken cancellationToken = default)
        {
            return base.GetByIdAsync(new object[] { forecastRunId, horizonDays }, cancellationToken);
        }
    }
}
