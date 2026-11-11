using AIMS.Entities;
using AIMS.DataAccess.Interface;
using AIMS.Facade.Interface;

namespace AIMS.Facade.Implementation
{
    public sealed class ProductBatchFacade : IProductBatchFacade
    {
        private readonly IProductBatchRepository _productBatchRepository;

        public ProductBatchFacade(IProductBatchRepository productBatchRepository)
        {
            ArgumentNullException.ThrowIfNull(productBatchRepository);
            _productBatchRepository = productBatchRepository;
        }

        public Task<List<ProductBatch>> GetProductBatchesAsync(CancellationToken cancellationToken = default)
        {
            return _productBatchRepository.GetAllAsync(cancellationToken);
        }

        public Task<ProductBatch?> GetProductBatchByIdAsync(long productBatchId, CancellationToken cancellationToken = default)
        {
            return _productBatchRepository.GetByIdAsync(productBatchId, cancellationToken);
        }

        public Task<List<ProductBatch>> GetProductBatchesPageAsync(int skip, int take, CancellationToken cancellationToken = default)
        {
            return _productBatchRepository.GetPageAsync(skip, take, cancellationToken);
        }
    }
}
