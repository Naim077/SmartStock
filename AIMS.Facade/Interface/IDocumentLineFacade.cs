using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Interface
{
    public interface IDocumentLineFacade
    {
        Task<List<DocumentLine>> GetDocumentLinesAsync();
        Task<DocumentLine?> GetDocumentLineByIdAsync(long lineId);
        Task AddDocumentLineAsync(DocumentLine documentLine);
        Task<bool> UpdateDocumentLineAsync(DocumentLine documentLine);
        Task<bool> DeleteDocumentLineAsync(long lineId);
    }
}
