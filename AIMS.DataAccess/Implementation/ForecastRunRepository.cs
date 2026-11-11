using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Implementation
{
    public sealed class ForecastRunRepository : ReadOnlyRepository<ForecastRun>, IForecastRunRepository
    {
        public ForecastRunRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<ForecastRun?> GetByIdAsync(long forecastRunId, CancellationToken cancellationToken = default)
        {
            return base.GetByIdAsync(new object[] { forecastRunId }, cancellationToken);
        }
    }
}
