using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Interface
{
    public interface IStockChangeLineRepository : IReadOnlyRepository<StockChangeLine>
    {
        Task<StockChangeLine?> GetByIdAsync(long lineId, CancellationToken cancellationToken = default);
    }
}
