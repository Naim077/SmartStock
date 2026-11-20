using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Interface
{
    public interface IProductStockBalanceFacade
    {
        Task<List<ProductStockBalance>> GetProductStockBalancesAsync();
        Task<ProductStockBalance?> GetProductStockBalanceByIdAsync(long productId);
        Task AddProductStockBalanceAsync(ProductStockBalance productStockBalance);
        Task<bool> UpdateProductStockBalanceAsync(ProductStockBalance productStockBalance);
        Task<bool> DeleteProductStockBalanceAsync(long productId);
    }
}
