using AIMS.Entities;

namespace AIMS.Facade.Interface
{
    /// <summary>Read-only, detached queries for workflow-managed BusinessSettings records.</summary>
    /// <remarks>Independent mutations are not exposed; changes require the owning workflow.</remarks>
    public interface IBusinessSettingsFacade
    {
        Task<List<BusinessSettings>> GetBusinessSettingsAsync(CancellationToken cancellationToken = default);
        Task<BusinessSettings?> GetBusinessSettingsByIdAsync(byte settingsId, CancellationToken cancellationToken = default);
        Task<List<BusinessSettings>> GetBusinessSettingsPageAsync(int skip, int take, CancellationToken cancellationToken = default);
    }
}
