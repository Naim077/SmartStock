using AIMS.Entities;
using AIMS.DataAccess.Interface;
using AIMS.Facade.Interface;

namespace AIMS.Facade.Implementation
{
    public sealed class StockBalanceFacade : IStockBalanceFacade
    {
        private readonly IStockBalanceRepository _stockBalanceRepository;

        public StockBalanceFacade(IStockBalanceRepository stockBalanceRepository)
        {
            ArgumentNullException.ThrowIfNull(stockBalanceRepository);
            _stockBalanceRepository = stockBalanceRepository;
        }

        public Task<List<StockBalance>> GetStockBalancesAsync(CancellationToken cancellationToken = default)
        {
            return _stockBalanceRepository.GetAllAsync(cancellationToken);
        }

        public Task<StockBalance?> GetStockBalanceByIdAsync(long productBatchId, string bucket, CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(bucket);
            return _stockBalanceRepository.GetByIdAsync(productBatchId, bucket, cancellationToken);
        }

        public Task<List<StockBalance>> GetStockBalancesPageAsync(int skip, int take, CancellationToken cancellationToken = default)
        {
            return _stockBalanceRepository.GetPageAsync(skip, take, cancellationToken);
        }
    }
}
