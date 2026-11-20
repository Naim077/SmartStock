using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Interface
{
    public interface IProductBatchRepository
    {
        Task<List<ProductBatch>> GetAllAsync();
        Task<ProductBatch?> GetByIdAsync(long productBatchId);
        Task AddAsync(ProductBatch productBatch);
        Task UpdateAsync(ProductBatch productBatch);
        Task DeleteAsync(long productBatchId);
    }
}
