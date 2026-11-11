using AIMS.Entities;
using AIMS.DataAccess.Interface;
using AIMS.Facade.Interface;

namespace AIMS.Facade.Implementation
{
    public sealed class ForecastObservationFacade : IForecastObservationFacade
    {
        private readonly IForecastObservationRepository _forecastObservationRepository;

        public ForecastObservationFacade(IForecastObservationRepository forecastObservationRepository)
        {
            ArgumentNullException.ThrowIfNull(forecastObservationRepository);
            _forecastObservationRepository = forecastObservationRepository;
        }

        public Task<List<ForecastObservation>> GetForecastObservationsAsync(CancellationToken cancellationToken = default)
        {
            return _forecastObservationRepository.GetAllAsync(cancellationToken);
        }

        public Task<ForecastObservation?> GetForecastObservationByIdAsync(long forecastRunId, string snapshotPhase, DateOnly observationDate, CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(snapshotPhase);
            return _forecastObservationRepository.GetByIdAsync(forecastRunId, snapshotPhase, observationDate, cancellationToken);
        }

        public Task<List<ForecastObservation>> GetForecastObservationsPageAsync(int skip, int take, CancellationToken cancellationToken = default)
        {
            return _forecastObservationRepository.GetPageAsync(skip, take, cancellationToken);
        }
    }
}
