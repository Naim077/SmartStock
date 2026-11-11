using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Interface
{
    public interface IStockMovementRepository : IReadOnlyRepository<StockMovement>
    {
        Task<StockMovement?> GetByIdAsync(long stockMovementId, CancellationToken cancellationToken = default);
    }
}
