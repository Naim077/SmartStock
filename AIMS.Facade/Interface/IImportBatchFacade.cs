using AIMS.Entities;

namespace AIMS.Facade.Interface
{
    /// <summary>Read-only, detached queries for workflow-managed ImportBatch records.</summary>
    /// <remarks>Independent mutations are not exposed; changes require the owning workflow.</remarks>
    public interface IImportBatchFacade
    {
        Task<List<ImportBatch>> GetImportBatchesAsync(CancellationToken cancellationToken = default);
        Task<ImportBatch?> GetImportBatchByIdAsync(long importBatchId, CancellationToken cancellationToken = default);
        Task<List<ImportBatch>> GetImportBatchesPageAsync(int skip, int take, CancellationToken cancellationToken = default);
    }
}
