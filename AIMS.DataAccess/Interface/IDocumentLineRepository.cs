using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Interface
{
    public interface IDocumentLineRepository
    {
        Task<List<DocumentLine>> GetAllAsync();
        Task<DocumentLine?> GetByIdAsync(long lineId);
        Task AddAsync(DocumentLine documentLine);
        Task UpdateAsync(DocumentLine documentLine);
        Task DeleteAsync(long lineId);
    }
}
