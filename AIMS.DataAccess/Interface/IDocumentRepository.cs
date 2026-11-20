using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Interface
{
    public interface IDocumentRepository
    {
        Task<List<Document>> GetAllAsync();
        Task<Document?> GetByIdAsync(long documentId);
        Task AddAsync(Document document);
        Task UpdateAsync(Document document);
        Task DeleteAsync(long documentId);
    }
}
