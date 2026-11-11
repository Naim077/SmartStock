using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Interface
{
    public interface IStockBalanceRepository : IReadOnlyRepository<StockBalance>
    {
        Task<StockBalance?> GetByIdAsync(long productBatchId, string bucket, CancellationToken cancellationToken = default);
    }
}
