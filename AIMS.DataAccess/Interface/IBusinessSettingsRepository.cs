using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Interface
{
    public interface IBusinessSettingsRepository : IReadOnlyRepository<BusinessSettings>
    {
        Task<BusinessSettings?> GetByIdAsync(byte settingsId, CancellationToken cancellationToken = default);
    }
}
