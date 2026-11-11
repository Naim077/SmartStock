using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Interface
{
    public interface IForecastPointRepository : IReadOnlyRepository<ForecastPoint>
    {
        Task<ForecastPoint?> GetByIdAsync(long forecastRunId, short horizonDays, CancellationToken cancellationToken = default);
    }
}
