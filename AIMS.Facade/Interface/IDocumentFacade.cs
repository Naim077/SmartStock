using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Interface
{
    public interface IDocumentFacade
    {
        Task<List<Document>> GetDocumentsAsync();
        Task<Document?> GetDocumentByIdAsync(long documentId);
        Task AddDocumentAsync(Document document);
        Task<bool> UpdateDocumentAsync(Document document);
        Task<bool> DeleteDocumentAsync(long documentId);
    }
}
