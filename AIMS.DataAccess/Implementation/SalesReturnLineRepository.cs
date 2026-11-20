using AIMS.DataAccess.Interface;
using AIMS.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Implementation
{
    public class SalesReturnLineRepository : ISalesReturnLineRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SalesReturnLineRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<SalesReturnLine>> GetAllAsync()
        {
            return await _dbContext.SalesReturnLines.ToListAsync();
        }

        public async Task<SalesReturnLine?> GetByIdAsync(long lineId)
        {
            return await _dbContext.SalesReturnLines.FindAsync(new object[] { lineId });
        }

        public async Task AddAsync(SalesReturnLine salesReturnLine)
        {
            await _dbContext.SalesReturnLines.AddAsync(salesReturnLine);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(SalesReturnLine salesReturnLine)
        {
            _dbContext.SalesReturnLines.Update(salesReturnLine);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(long lineId)
        {
            var salesReturnLine = await _dbContext.SalesReturnLines.FindAsync(new object[] { lineId });
            if (salesReturnLine == null)
                return;

            _dbContext.SalesReturnLines.Remove(salesReturnLine);
            await _dbContext.SaveChangesAsync();
        }
    }
}
