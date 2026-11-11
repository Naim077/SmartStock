using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Implementation
{
    public sealed class ForecastMetricRepository : ReadOnlyRepository<ForecastMetric>, IForecastMetricRepository
    {
        public ForecastMetricRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<ForecastMetric?> GetByIdAsync(long forecastRunId, string method, CancellationToken cancellationToken = default)
        {
            return base.GetByIdAsync(new object[] { forecastRunId, method }, cancellationToken);
        }
    }
}
