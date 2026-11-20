using AIMS.DataAccess.Interface;
using AIMS.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Implementation
{
    public class SalesReturnRepository : ISalesReturnRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SalesReturnRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<SalesReturn>> GetAllAsync()
        {
            return await _dbContext.SalesReturns.ToListAsync();
        }

        public async Task<SalesReturn?> GetByIdAsync(long documentId)
        {
            return await _dbContext.SalesReturns.FindAsync(new object[] { documentId });
        }

        public async Task AddAsync(SalesReturn salesReturn)
        {
            await _dbContext.SalesReturns.AddAsync(salesReturn);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(SalesReturn salesReturn)
        {
            _dbContext.SalesReturns.Update(salesReturn);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(long documentId)
        {
            var salesReturn = await _dbContext.SalesReturns.FindAsync(new object[] { documentId });
            if (salesReturn == null)
                return;

            _dbContext.SalesReturns.Remove(salesReturn);
            await _dbContext.SaveChangesAsync();
        }
    }
}
