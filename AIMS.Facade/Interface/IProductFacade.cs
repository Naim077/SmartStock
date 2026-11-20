using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Interface
{
    public interface IProductFacade
    {
        Task<List<Product>> GetProductsAsync();
        Task<Product?> GetProductByIdAsync(long productId);
        Task AddProductAsync(Product product);
        Task<bool> UpdateProductAsync(Product product);
        Task<bool> DeleteProductAsync(long productId);
    }
}
