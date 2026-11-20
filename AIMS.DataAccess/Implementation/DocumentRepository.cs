using AIMS.DataAccess.Interface;
using AIMS.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Implementation
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public DocumentRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Document>> GetAllAsync()
        {
            return await _dbContext.Documents.ToListAsync();
        }

        public async Task<Document?> GetByIdAsync(long documentId)
        {
            return await _dbContext.Documents.FindAsync(new object[] { documentId });
        }

        public async Task AddAsync(Document document)
        {
            await _dbContext.Documents.AddAsync(document);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Document document)
        {
            _dbContext.Documents.Update(document);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(long documentId)
        {
            var document = await _dbContext.Documents.FindAsync(new object[] { documentId });
            if (document == null)
                return;

            _dbContext.Documents.Remove(document);
            await _dbContext.SaveChangesAsync();
        }
    }
}
