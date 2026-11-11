using AIMS.Entities;
using AIMS.DataAccess.Interface;
using AIMS.Facade.Interface;

namespace AIMS.Facade.Implementation
{
    public sealed class BusinessSettingsFacade : IBusinessSettingsFacade
    {
        private readonly IBusinessSettingsRepository _businessSettingsRepository;

        public BusinessSettingsFacade(IBusinessSettingsRepository businessSettingsRepository)
        {
            ArgumentNullException.ThrowIfNull(businessSettingsRepository);
            _businessSettingsRepository = businessSettingsRepository;
        }

        public Task<List<BusinessSettings>> GetBusinessSettingsAsync(CancellationToken cancellationToken = default)
        {
            return _businessSettingsRepository.GetAllAsync(cancellationToken);
        }

        public Task<BusinessSettings?> GetBusinessSettingsByIdAsync(byte settingsId, CancellationToken cancellationToken = default)
        {
            return _businessSettingsRepository.GetByIdAsync(settingsId, cancellationToken);
        }

        public Task<List<BusinessSettings>> GetBusinessSettingsPageAsync(int skip, int take, CancellationToken cancellationToken = default)
        {
            return _businessSettingsRepository.GetPageAsync(skip, take, cancellationToken);
        }
    }
}
