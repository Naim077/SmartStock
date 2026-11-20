using AIMS.DataAccess.Interface;
using AIMS.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Implementation
{
    public class ProductStockBalanceRepository : IProductStockBalanceRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductStockBalanceRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ProductStockBalance>> GetAllAsync()
        {
            return await _dbContext.ProductStockBalances.ToListAsync();
        }

        public async Task<ProductStockBalance?> GetByIdAsync(long productId)
        {
            return await _dbContext.ProductStockBalances.FindAsync(new object[] { productId });
        }

        public async Task AddAsync(ProductStockBalance productStockBalance)
        {
            await _dbContext.ProductStockBalances.AddAsync(productStockBalance);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProductStockBalance productStockBalance)
        {
            _dbContext.ProductStockBalances.Update(productStockBalance);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(long productId)
        {
            var productStockBalance = await _dbContext.ProductStockBalances.FindAsync(new object[] { productId });
            if (productStockBalance == null)
                return;

            _dbContext.ProductStockBalances.Remove(productStockBalance);
            await _dbContext.SaveChangesAsync();
        }
    }
}
