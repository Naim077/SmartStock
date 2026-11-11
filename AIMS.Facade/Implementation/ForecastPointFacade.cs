using AIMS.Entities;
using AIMS.DataAccess.Interface;
using AIMS.Facade.Interface;

namespace AIMS.Facade.Implementation
{
    public sealed class ForecastPointFacade : IForecastPointFacade
    {
        private readonly IForecastPointRepository _forecastPointRepository;

        public ForecastPointFacade(IForecastPointRepository forecastPointRepository)
        {
            ArgumentNullException.ThrowIfNull(forecastPointRepository);
            _forecastPointRepository = forecastPointRepository;
        }

        public Task<List<ForecastPoint>> GetForecastPointsAsync(CancellationToken cancellationToken = default)
        {
            return _forecastPointRepository.GetAllAsync(cancellationToken);
        }

        public Task<ForecastPoint?> GetForecastPointByIdAsync(long forecastRunId, short horizonDays, CancellationToken cancellationToken = default)
        {
            return _forecastPointRepository.GetByIdAsync(forecastRunId, horizonDays, cancellationToken);
        }

        public Task<List<ForecastPoint>> GetForecastPointsPageAsync(int skip, int take, CancellationToken cancellationToken = default)
        {
            return _forecastPointRepository.GetPageAsync(skip, take, cancellationToken);
        }
    }
}
