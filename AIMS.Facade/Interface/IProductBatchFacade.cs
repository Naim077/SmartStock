using AIMS.Entities;

namespace AIMS.Facade.Interface
{
    /// <summary>Read-only, detached queries for workflow-managed ProductBatch records.</summary>
    /// <remarks>Independent mutations are not exposed; changes require the owning workflow.</remarks>
    public interface IProductBatchFacade
    {
        Task<List<ProductBatch>> GetProductBatchesAsync(CancellationToken cancellationToken = default);
        Task<ProductBatch?> GetProductBatchByIdAsync(long productBatchId, CancellationToken cancellationToken = default);
        Task<List<ProductBatch>> GetProductBatchesPageAsync(int skip, int take, CancellationToken cancellationToken = default);
    }
}
