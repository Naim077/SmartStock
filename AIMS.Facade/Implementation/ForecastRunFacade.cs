using AIMS.Entities;
using AIMS.DataAccess.Interface;
using AIMS.Facade.Interface;

namespace AIMS.Facade.Implementation
{
    public sealed class ForecastRunFacade : IForecastRunFacade
    {
        private readonly IForecastRunRepository _forecastRunRepository;

        public ForecastRunFacade(IForecastRunRepository forecastRunRepository)
        {
            ArgumentNullException.ThrowIfNull(forecastRunRepository);
            _forecastRunRepository = forecastRunRepository;
        }

        public Task<List<ForecastRun>> GetForecastRunsAsync(CancellationToken cancellationToken = default)
        {
            return _forecastRunRepository.GetAllAsync(cancellationToken);
        }

        public Task<ForecastRun?> GetForecastRunByIdAsync(long forecastRunId, CancellationToken cancellationToken = default)
        {
            return _forecastRunRepository.GetByIdAsync(forecastRunId, cancellationToken);
        }

        public Task<List<ForecastRun>> GetForecastRunsPageAsync(int skip, int take, CancellationToken cancellationToken = default)
        {
            return _forecastRunRepository.GetPageAsync(skip, take, cancellationToken);
        }
    }
}
