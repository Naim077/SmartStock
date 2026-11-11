using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Interface
{
    public interface IDocumentRepository : IReadOnlyRepository<Document>
    {
        Task<Document?> GetByIdAsync(long documentId, CancellationToken cancellationToken = default);
    }
}
