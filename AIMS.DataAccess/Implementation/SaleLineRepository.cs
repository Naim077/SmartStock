using AIMS.DataAccess.Interface;
using AIMS.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Implementation
{
    public class SaleLineRepository : ISaleLineRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SaleLineRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<SaleLine>> GetAllAsync()
        {
            return await _dbContext.SaleLines.ToListAsync();
        }

        public async Task<SaleLine?> GetByIdAsync(long lineId)
        {
            return await _dbContext.SaleLines.FindAsync(new object[] { lineId });
        }

        public async Task AddAsync(SaleLine saleLine)
        {
            await _dbContext.SaleLines.AddAsync(saleLine);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(SaleLine saleLine)
        {
            _dbContext.SaleLines.Update(saleLine);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(long lineId)
        {
            var saleLine = await _dbContext.SaleLines.FindAsync(new object[] { lineId });
            if (saleLine == null)
                return;

            _dbContext.SaleLines.Remove(saleLine);
            await _dbContext.SaveChangesAsync();
        }
    }
}
