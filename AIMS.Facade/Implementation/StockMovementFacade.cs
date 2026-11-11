using AIMS.Entities;
using AIMS.DataAccess.Interface;
using AIMS.Facade.Interface;

namespace AIMS.Facade.Implementation
{
    public sealed class StockMovementFacade : IStockMovementFacade
    {
        private readonly IStockMovementRepository _stockMovementRepository;

        public StockMovementFacade(IStockMovementRepository stockMovementRepository)
        {
            ArgumentNullException.ThrowIfNull(stockMovementRepository);
            _stockMovementRepository = stockMovementRepository;
        }

        public Task<List<StockMovement>> GetStockMovementsAsync(CancellationToken cancellationToken = default)
        {
            return _stockMovementRepository.GetAllAsync(cancellationToken);
        }

        public Task<StockMovement?> GetStockMovementByIdAsync(long stockMovementId, CancellationToken cancellationToken = default)
        {
            return _stockMovementRepository.GetByIdAsync(stockMovementId, cancellationToken);
        }

        public Task<List<StockMovement>> GetStockMovementsPageAsync(int skip, int take, CancellationToken cancellationToken = default)
        {
            return _stockMovementRepository.GetPageAsync(skip, take, cancellationToken);
        }
    }
}
