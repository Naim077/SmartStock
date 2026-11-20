using AIMS.DataAccess.Interface;
using AIMS.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Implementation
{
    public class DocumentLineRepository : IDocumentLineRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public DocumentLineRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<DocumentLine>> GetAllAsync()
        {
            return await _dbContext.DocumentLines.ToListAsync();
        }

        public async Task<DocumentLine?> GetByIdAsync(long lineId)
        {
            return await _dbContext.DocumentLines.FindAsync(new object[] { lineId });
        }

        public async Task AddAsync(DocumentLine documentLine)
        {
            await _dbContext.DocumentLines.AddAsync(documentLine);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(DocumentLine documentLine)
        {
            _dbContext.DocumentLines.Update(documentLine);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(long lineId)
        {
            var documentLine = await _dbContext.DocumentLines.FindAsync(new object[] { lineId });
            if (documentLine == null)
                return;

            _dbContext.DocumentLines.Remove(documentLine);
            await _dbContext.SaveChangesAsync();
        }
    }
}
