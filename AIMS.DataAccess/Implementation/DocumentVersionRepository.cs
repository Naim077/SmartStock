using AIMS.DataAccess.Interface;
using AIMS.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Implementation
{
    public class DocumentVersionRepository : IDocumentVersionRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public DocumentVersionRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<DocumentVersion>> GetAllAsync()
        {
            return await _dbContext.DocumentVersions.ToListAsync();
        }

        public async Task<DocumentVersion?> GetByIdAsync(long documentId, int versionNo)
        {
            return await _dbContext.DocumentVersions.FindAsync(new object[] { documentId, versionNo });
        }

        public async Task AddAsync(DocumentVersion documentVersion)
        {
            await _dbContext.DocumentVersions.AddAsync(documentVersion);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(DocumentVersion documentVersion)
        {
            _dbContext.DocumentVersions.Update(documentVersion);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(long documentId, int versionNo)
        {
            var documentVersion = await _dbContext.DocumentVersions.FindAsync(new object[] { documentId, versionNo });
            if (documentVersion == null)
                return;

            _dbContext.DocumentVersions.Remove(documentVersion);
            await _dbContext.SaveChangesAsync();
        }
    }
}
