using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Interface
{
    public interface IAlertRepository
    {
        Task<List<Alert>> GetAllAsync();
        Task<Alert?> GetByIdAsync(long alertId);
        Task AddAsync(Alert alert);
        Task UpdateAsync(Alert alert);
        Task DeleteAsync(long alertId);
    }
}
