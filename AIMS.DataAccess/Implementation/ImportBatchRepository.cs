using AIMS.DataAccess.Interface;
using AIMS.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Implementation
{
    public class ImportBatchRepository : IImportBatchRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ImportBatchRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ImportBatch>> GetAllAsync()
        {
            return await _dbContext.ImportBatches.ToListAsync();
        }

        public async Task<ImportBatch?> GetByIdAsync(long importBatchId)
        {
            return await _dbContext.ImportBatches.FindAsync(new object[] { importBatchId });
        }

        public async Task AddAsync(ImportBatch importBatch)
        {
            await _dbContext.ImportBatches.AddAsync(importBatch);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(ImportBatch importBatch)
        {
            _dbContext.ImportBatches.Update(importBatch);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(long importBatchId)
        {
            var importBatch = await _dbContext.ImportBatches.FindAsync(new object[] { importBatchId });
            if (importBatch == null)
                return;

            _dbContext.ImportBatches.Remove(importBatch);
            await _dbContext.SaveChangesAsync();
        }
    }
}
