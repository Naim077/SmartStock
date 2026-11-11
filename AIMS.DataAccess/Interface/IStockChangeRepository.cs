using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Interface
{
    public interface IStockChangeRepository : IReadOnlyRepository<StockChange>
    {
        Task<StockChange?> GetByIdAsync(long documentId, CancellationToken cancellationToken = default);
    }
}
