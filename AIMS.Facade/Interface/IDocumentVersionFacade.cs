using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Interface
{
    public interface IDocumentVersionFacade
    {
        Task<List<DocumentVersion>> GetDocumentVersionsAsync();
        Task<DocumentVersion?> GetDocumentVersionByIdAsync(long documentId, int versionNo);
        Task AddDocumentVersionAsync(DocumentVersion documentVersion);
        Task<bool> UpdateDocumentVersionAsync(DocumentVersion documentVersion);
        Task<bool> DeleteDocumentVersionAsync(long documentId, int versionNo);
    }
}
