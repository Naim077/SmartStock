using AIMS.Entities;
using AIMS.DataAccess.Interface;
using AIMS.Facade.Interface;

namespace AIMS.Facade.Implementation
{
    public sealed class ForecastMetricFacade : IForecastMetricFacade
    {
        private readonly IForecastMetricRepository _forecastMetricRepository;

        public ForecastMetricFacade(IForecastMetricRepository forecastMetricRepository)
        {
            ArgumentNullException.ThrowIfNull(forecastMetricRepository);
            _forecastMetricRepository = forecastMetricRepository;
        }

        public Task<List<ForecastMetric>> GetForecastMetricsAsync(CancellationToken cancellationToken = default)
        {
            return _forecastMetricRepository.GetAllAsync(cancellationToken);
        }

        public Task<ForecastMetric?> GetForecastMetricByIdAsync(long forecastRunId, string method, CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(method);
            return _forecastMetricRepository.GetByIdAsync(forecastRunId, method, cancellationToken);
        }

        public Task<List<ForecastMetric>> GetForecastMetricsPageAsync(int skip, int take, CancellationToken cancellationToken = default)
        {
            return _forecastMetricRepository.GetPageAsync(skip, take, cancellationToken);
        }
    }
}
