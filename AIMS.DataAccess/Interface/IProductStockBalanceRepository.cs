using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Interface
{
    public interface IProductStockBalanceRepository : IReadOnlyRepository<ProductStockBalance>
    {
        Task<ProductStockBalance?> GetByIdAsync(long productId, CancellationToken cancellationToken = default);
    }
}
