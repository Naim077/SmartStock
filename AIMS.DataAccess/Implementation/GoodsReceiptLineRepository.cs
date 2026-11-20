using AIMS.DataAccess.Interface;
using AIMS.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Implementation
{
    public class GoodsReceiptLineRepository : IGoodsReceiptLineRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public GoodsReceiptLineRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<GoodsReceiptLine>> GetAllAsync()
        {
            return await _dbContext.GoodsReceiptLines.ToListAsync();
        }

        public async Task<GoodsReceiptLine?> GetByIdAsync(long lineId)
        {
            return await _dbContext.GoodsReceiptLines.FindAsync(new object[] { lineId });
        }

        public async Task AddAsync(GoodsReceiptLine goodsReceiptLine)
        {
            await _dbContext.GoodsReceiptLines.AddAsync(goodsReceiptLine);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(GoodsReceiptLine goodsReceiptLine)
        {
            _dbContext.GoodsReceiptLines.Update(goodsReceiptLine);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(long lineId)
        {
            var goodsReceiptLine = await _dbContext.GoodsReceiptLines.FindAsync(new object[] { lineId });
            if (goodsReceiptLine == null)
                return;

            _dbContext.GoodsReceiptLines.Remove(goodsReceiptLine);
            await _dbContext.SaveChangesAsync();
        }
    }
}
