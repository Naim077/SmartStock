using AIMS.Entities;
using AIMS.DataAccess.Interface;
using AIMS.Facade.Interface;

namespace AIMS.Facade.Implementation
{
    public sealed class ProductStockBalanceFacade : IProductStockBalanceFacade
    {
        private readonly IProductStockBalanceRepository _productStockBalanceRepository;

        public ProductStockBalanceFacade(IProductStockBalanceRepository productStockBalanceRepository)
        {
            ArgumentNullException.ThrowIfNull(productStockBalanceRepository);
            _productStockBalanceRepository = productStockBalanceRepository;
        }

        public Task<List<ProductStockBalance>> GetProductStockBalancesAsync(CancellationToken cancellationToken = default)
        {
            return _productStockBalanceRepository.GetAllAsync(cancellationToken);
        }

        public Task<ProductStockBalance?> GetProductStockBalanceByIdAsync(long productId, CancellationToken cancellationToken = default)
        {
            return _productStockBalanceRepository.GetByIdAsync(productId, cancellationToken);
        }

        public Task<List<ProductStockBalance>> GetProductStockBalancesPageAsync(int skip, int take, CancellationToken cancellationToken = default)
        {
            return _productStockBalanceRepository.GetPageAsync(skip, take, cancellationToken);
        }
    }
}
