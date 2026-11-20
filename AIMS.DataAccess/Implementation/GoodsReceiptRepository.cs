using AIMS.DataAccess.Interface;
using AIMS.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Implementation
{
    public class GoodsReceiptRepository : IGoodsReceiptRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public GoodsReceiptRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<GoodsReceipt>> GetAllAsync()
        {
            return await _dbContext.GoodsReceipts.ToListAsync();
        }

        public async Task<GoodsReceipt?> GetByIdAsync(long documentId)
        {
            return await _dbContext.GoodsReceipts.FindAsync(new object[] { documentId });
        }

        public async Task AddAsync(GoodsReceipt goodsReceipt)
        {
            await _dbContext.GoodsReceipts.AddAsync(goodsReceipt);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(GoodsReceipt goodsReceipt)
        {
            _dbContext.GoodsReceipts.Update(goodsReceipt);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(long documentId)
        {
            var goodsReceipt = await _dbContext.GoodsReceipts.FindAsync(new object[] { documentId });
            if (goodsReceipt == null)
                return;

            _dbContext.GoodsReceipts.Remove(goodsReceipt);
            await _dbContext.SaveChangesAsync();
        }
    }
}
