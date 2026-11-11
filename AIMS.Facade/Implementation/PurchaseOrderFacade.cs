using AIMS.Entities;
using AIMS.DataAccess.Interface;
using AIMS.Facade.Interface;

namespace AIMS.Facade.Implementation
{
    public sealed class PurchaseOrderFacade : IPurchaseOrderFacade
    {
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;

        public PurchaseOrderFacade(IPurchaseOrderRepository purchaseOrderRepository)
        {
            ArgumentNullException.ThrowIfNull(purchaseOrderRepository);
            _purchaseOrderRepository = purchaseOrderRepository;
        }

        public Task<List<PurchaseOrder>> GetPurchaseOrdersAsync(CancellationToken cancellationToken = default)
        {
            return _purchaseOrderRepository.GetAllAsync(cancellationToken);
        }

        public Task<PurchaseOrder?> GetPurchaseOrderByIdAsync(long documentId, CancellationToken cancellationToken = default)
        {
            return _purchaseOrderRepository.GetByIdAsync(documentId, cancellationToken);
        }

        public Task<List<PurchaseOrder>> GetPurchaseOrdersPageAsync(int skip, int take, CancellationToken cancellationToken = default)
        {
            return _purchaseOrderRepository.GetPageAsync(skip, take, cancellationToken);
        }
    }
}
