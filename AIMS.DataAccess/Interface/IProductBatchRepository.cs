using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Interface
{
    public interface IProductBatchRepository : IReadOnlyRepository<ProductBatch>
    {
        Task<ProductBatch?> GetByIdAsync(long productBatchId, CancellationToken cancellationToken = default);
    }
}
