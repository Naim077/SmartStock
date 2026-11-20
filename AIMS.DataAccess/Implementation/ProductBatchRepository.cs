using AIMS.DataAccess.Interface;
using AIMS.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Implementation
{
    public class ProductBatchRepository : IProductBatchRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductBatchRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ProductBatch>> GetAllAsync()
        {
            return await _dbContext.ProductBatches.ToListAsync();
        }

        public async Task<ProductBatch?> GetByIdAsync(long productBatchId)
        {
            return await _dbContext.ProductBatches.FindAsync(new object[] { productBatchId });
        }

        public async Task AddAsync(ProductBatch productBatch)
        {
            await _dbContext.ProductBatches.AddAsync(productBatch);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProductBatch productBatch)
        {
            _dbContext.ProductBatches.Update(productBatch);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(long productBatchId)
        {
            var productBatch = await _dbContext.ProductBatches.FindAsync(new object[] { productBatchId });
            if (productBatch == null)
                return;

            _dbContext.ProductBatches.Remove(productBatch);
            await _dbContext.SaveChangesAsync();
        }
    }
}
