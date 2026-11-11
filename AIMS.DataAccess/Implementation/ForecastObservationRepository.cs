using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Implementation
{
    public sealed class ForecastObservationRepository : ReadOnlyRepository<ForecastObservation>, IForecastObservationRepository
    {
        public ForecastObservationRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<ForecastObservation?> GetByIdAsync(long forecastRunId, string snapshotPhase, DateOnly observationDate, CancellationToken cancellationToken = default)
        {
            return base.GetByIdAsync(new object[] { forecastRunId, snapshotPhase, observationDate }, cancellationToken);
        }
    }
}
