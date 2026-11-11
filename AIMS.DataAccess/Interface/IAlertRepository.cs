using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Interface
{
    public interface IAlertRepository : IReadOnlyRepository<Alert>
    {
        Task<Alert?> GetByIdAsync(long alertId, CancellationToken cancellationToken = default);
    }
}
