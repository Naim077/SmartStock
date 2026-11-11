using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Interface
{
    public interface IForecastRunRepository : IReadOnlyRepository<ForecastRun>
    {
        Task<ForecastRun?> GetByIdAsync(long forecastRunId, CancellationToken cancellationToken = default);
    }
}
