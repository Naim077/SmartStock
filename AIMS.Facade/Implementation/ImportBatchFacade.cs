using AIMS.DataAccess.Interface;
using AIMS.Entities;
using AIMS.Facade.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Implementation
{
    public class ImportBatchFacade : IImportBatchFacade
    {
        private readonly IImportBatchRepository _importBatchRepository;

        public ImportBatchFacade(IImportBatchRepository importBatchRepository)
        {
            _importBatchRepository = importBatchRepository;
        }

        public async Task<List<ImportBatch>> GetImportBatchesAsync()
        {
            return await _importBatchRepository.GetAllAsync();
        }

        public async Task<ImportBatch?> GetImportBatchByIdAsync(long importBatchId)
        {
            return await _importBatchRepository.GetByIdAsync(importBatchId);
        }

        public async Task AddImportBatchAsync(ImportBatch importBatch)
        {
            ArgumentNullException.ThrowIfNull(importBatch);
            await _importBatchRepository.AddAsync(importBatch);
        }

        public async Task<bool> UpdateImportBatchAsync(ImportBatch importBatch)
        {
            ArgumentNullException.ThrowIfNull(importBatch);
            var existing = await _importBatchRepository.GetByIdAsync(importBatch.ImportBatchId);
            if (existing == null)
                return false;

            await _importBatchRepository.UpdateAsync(importBatch);
            return true;
        }

        public async Task<bool> DeleteImportBatchAsync(long importBatchId)
        {
            var existing = await _importBatchRepository.GetByIdAsync(importBatchId);
            if (existing == null)
                return false;

            await _importBatchRepository.DeleteAsync(importBatchId);
            return true;
        }
    }
}
