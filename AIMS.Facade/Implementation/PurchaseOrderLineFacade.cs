using AIMS.Entities;
using AIMS.DataAccess.Interface;
using AIMS.Facade.Interface;

namespace AIMS.Facade.Implementation
{
    public sealed class PurchaseOrderLineFacade : IPurchaseOrderLineFacade
    {
        private readonly IPurchaseOrderLineRepository _purchaseOrderLineRepository;

        public PurchaseOrderLineFacade(IPurchaseOrderLineRepository purchaseOrderLineRepository)
        {
            ArgumentNullException.ThrowIfNull(purchaseOrderLineRepository);
            _purchaseOrderLineRepository = purchaseOrderLineRepository;
        }

        public Task<List<PurchaseOrderLine>> GetPurchaseOrderLinesAsync(CancellationToken cancellationToken = default)
        {
            return _purchaseOrderLineRepository.GetAllAsync(cancellationToken);
        }

        public Task<PurchaseOrderLine?> GetPurchaseOrderLineByIdAsync(long lineId, CancellationToken cancellationToken = default)
        {
            return _purchaseOrderLineRepository.GetByIdAsync(lineId, cancellationToken);
        }

        public Task<List<PurchaseOrderLine>> GetPurchaseOrderLinesPageAsync(int skip, int take, CancellationToken cancellationToken = default)
        {
            return _purchaseOrderLineRepository.GetPageAsync(skip, take, cancellationToken);
        }
    }
}
