using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Interface
{
    public interface IDocumentVersionRepository
    {
        Task<List<DocumentVersion>> GetAllAsync();
        Task<DocumentVersion?> GetByIdAsync(long documentId, int versionNo);
        Task AddAsync(DocumentVersion documentVersion);
        Task UpdateAsync(DocumentVersion documentVersion);
        Task DeleteAsync(long documentId, int versionNo);
    }
}
