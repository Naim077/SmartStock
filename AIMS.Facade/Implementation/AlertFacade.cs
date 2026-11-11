using AIMS.Entities;
using AIMS.DataAccess.Interface;
using AIMS.Facade.Interface;

namespace AIMS.Facade.Implementation
{
    public sealed class AlertFacade : IAlertFacade
    {
        private readonly IAlertRepository _alertRepository;

        public AlertFacade(IAlertRepository alertRepository)
        {
            ArgumentNullException.ThrowIfNull(alertRepository);
            _alertRepository = alertRepository;
        }

        public Task<List<Alert>> GetAlertsAsync(CancellationToken cancellationToken = default)
        {
            return _alertRepository.GetAllAsync(cancellationToken);
        }

        public Task<Alert?> GetAlertByIdAsync(long alertId, CancellationToken cancellationToken = default)
        {
            return _alertRepository.GetByIdAsync(alertId, cancellationToken);
        }

        public Task<List<Alert>> GetAlertsPageAsync(int skip, int take, CancellationToken cancellationToken = default)
        {
            return _alertRepository.GetPageAsync(skip, take, cancellationToken);
        }
    }
}
