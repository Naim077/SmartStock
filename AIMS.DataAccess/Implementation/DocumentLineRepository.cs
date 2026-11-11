using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Implementation
{
    public sealed class DocumentLineRepository : ReadOnlyRepository<DocumentLine>, IDocumentLineRepository
    {
        public DocumentLineRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<DocumentLine?> GetByIdAsync(long lineId, CancellationToken cancellationToken = default)
        {
            return base.GetByIdAsync(new object[] { lineId }, cancellationToken);
        }
    }
}
