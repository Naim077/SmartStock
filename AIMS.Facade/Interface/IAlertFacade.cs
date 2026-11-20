using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Interface
{
    public interface IAlertFacade
    {
        Task<List<Alert>> GetAlertsAsync();
        Task<Alert?> GetAlertByIdAsync(long alertId);
        Task AddAlertAsync(Alert alert);
        Task<bool> UpdateAlertAsync(Alert alert);
        Task<bool> DeleteAlertAsync(long alertId);
    }
}
