using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Implementation
{
    public sealed class ImportBatchRepository : ReadOnlyRepository<ImportBatch>, IImportBatchRepository
    {
        public ImportBatchRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<ImportBatch?> GetByIdAsync(long importBatchId, CancellationToken cancellationToken = default)
        {
            return base.GetByIdAsync(new object[] { importBatchId }, cancellationToken);
        }
    }
}
