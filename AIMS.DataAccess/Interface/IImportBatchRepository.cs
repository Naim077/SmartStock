using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Interface
{
    public interface IImportBatchRepository : IReadOnlyRepository<ImportBatch>
    {
        Task<ImportBatch?> GetByIdAsync(long importBatchId, CancellationToken cancellationToken = default);
    }
}
