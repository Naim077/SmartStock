using AIMS.Entities;
using AIMS.DataAccess.Interface;
using AIMS.Facade.Interface;

namespace AIMS.Facade.Implementation
{
    public sealed class ImportBatchFacade : IImportBatchFacade
    {
        private readonly IImportBatchRepository _importBatchRepository;

        public ImportBatchFacade(IImportBatchRepository importBatchRepository)
        {
            ArgumentNullException.ThrowIfNull(importBatchRepository);
            _importBatchRepository = importBatchRepository;
        }

        public Task<List<ImportBatch>> GetImportBatchesAsync(CancellationToken cancellationToken = default)
        {
            return _importBatchRepository.GetAllAsync(cancellationToken);
        }

        public Task<ImportBatch?> GetImportBatchByIdAsync(long importBatchId, CancellationToken cancellationToken = default)
        {
            return _importBatchRepository.GetByIdAsync(importBatchId, cancellationToken);
        }

        public Task<List<ImportBatch>> GetImportBatchesPageAsync(int skip, int take, CancellationToken cancellationToken = default)
        {
            return _importBatchRepository.GetPageAsync(skip, take, cancellationToken);
        }
    }
}
