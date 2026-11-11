using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Implementation
{
    public sealed class DocumentVersionRepository : ReadOnlyRepository<DocumentVersion>, IDocumentVersionRepository
    {
        public DocumentVersionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<DocumentVersion?> GetByIdAsync(long documentId, int versionNo, CancellationToken cancellationToken = default)
        {
            return base.GetByIdAsync(new object[] { documentId, versionNo }, cancellationToken);
        }
    }
}
