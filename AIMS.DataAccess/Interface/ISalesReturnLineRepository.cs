using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Interface
{
    public interface ISalesReturnLineRepository : IReadOnlyRepository<SalesReturnLine>
    {
        Task<SalesReturnLine?> GetByIdAsync(long lineId, CancellationToken cancellationToken = default);
    }
}
