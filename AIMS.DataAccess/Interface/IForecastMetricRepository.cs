using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Interface
{
    public interface IForecastMetricRepository : IReadOnlyRepository<ForecastMetric>
    {
        Task<ForecastMetric?> GetByIdAsync(long forecastRunId, string method, CancellationToken cancellationToken = default);
    }
}
