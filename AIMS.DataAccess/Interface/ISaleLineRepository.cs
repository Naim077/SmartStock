using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Interface
{
    public interface ISaleLineRepository : IReadOnlyRepository<SaleLine>
    {
        Task<SaleLine?> GetByIdAsync(long lineId, CancellationToken cancellationToken = default);
    }
}
