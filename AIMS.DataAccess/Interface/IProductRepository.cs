using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Interface
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product?> GetByIdAsync(long productId, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(long productId, byte[] rowVersion, CancellationToken cancellationToken = default);
    }
}
