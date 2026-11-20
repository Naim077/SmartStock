using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Interface
{
    public interface IProductBatchFacade
    {
        Task<List<ProductBatch>> GetProductBatchesAsync();
        Task<ProductBatch?> GetProductBatchByIdAsync(long productBatchId);
        Task AddProductBatchAsync(ProductBatch productBatch);
        Task<bool> UpdateProductBatchAsync(ProductBatch productBatch);
        Task<bool> DeleteProductBatchAsync(long productBatchId);
    }
}
