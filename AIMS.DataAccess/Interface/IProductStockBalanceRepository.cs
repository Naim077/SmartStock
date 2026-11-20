using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Interface
{
    public interface IProductStockBalanceRepository
    {
        Task<List<ProductStockBalance>> GetAllAsync();
        Task<ProductStockBalance?> GetByIdAsync(long productId);
        Task AddAsync(ProductStockBalance productStockBalance);
        Task UpdateAsync(ProductStockBalance productStockBalance);
        Task DeleteAsync(long productId);
    }
}
