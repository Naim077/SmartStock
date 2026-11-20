using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Interface
{
    public interface IImportBatchRepository
    {
        Task<List<ImportBatch>> GetAllAsync();
        Task<ImportBatch?> GetByIdAsync(long importBatchId);
        Task AddAsync(ImportBatch importBatch);
        Task UpdateAsync(ImportBatch importBatch);
        Task DeleteAsync(long importBatchId);
    }
}
