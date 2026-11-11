using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Implementation
{
    public sealed class DocumentRepository : ReadOnlyRepository<Document>, IDocumentRepository
    {
        public DocumentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<Document?> GetByIdAsync(long documentId, CancellationToken cancellationToken = default)
        {
            return base.GetByIdAsync(new object[] { documentId }, cancellationToken);
        }
    }
}
