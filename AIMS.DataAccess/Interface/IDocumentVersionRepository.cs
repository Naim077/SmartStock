using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Interface
{
    public interface IDocumentVersionRepository : IReadOnlyRepository<DocumentVersion>
    {
        Task<DocumentVersion?> GetByIdAsync(long documentId, int versionNo, CancellationToken cancellationToken = default);
    }
}
