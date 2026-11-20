using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Interface
{
    public interface IImportBatchFacade
    {
        Task<List<ImportBatch>> GetImportBatchesAsync();
        Task<ImportBatch?> GetImportBatchByIdAsync(long importBatchId);
        Task AddImportBatchAsync(ImportBatch importBatch);
        Task<bool> UpdateImportBatchAsync(ImportBatch importBatch);
        Task<bool> DeleteImportBatchAsync(long importBatchId);
    }
}
