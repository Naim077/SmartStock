using AIMS.Entities;
using AIMS.DataAccess.Interface;
using AIMS.Facade.Interface;

namespace AIMS.Facade.Implementation
{
    public sealed class StockChangeFacade : IStockChangeFacade
    {
        private readonly IStockChangeRepository _stockChangeRepository;

        public StockChangeFacade(IStockChangeRepository stockChangeRepository)
        {
            ArgumentNullException.ThrowIfNull(stockChangeRepository);
            _stockChangeRepository = stockChangeRepository;
        }

        public Task<List<StockChange>> GetStockChangesAsync(CancellationToken cancellationToken = default)
        {
            return _stockChangeRepository.GetAllAsync(cancellationToken);
        }

        public Task<StockChange?> GetStockChangeByIdAsync(long documentId, CancellationToken cancellationToken = default)
        {
            return _stockChangeRepository.GetByIdAsync(documentId, cancellationToken);
        }

        public Task<List<StockChange>> GetStockChangesPageAsync(int skip, int take, CancellationToken cancellationToken = default)
        {
            return _stockChangeRepository.GetPageAsync(skip, take, cancellationToken);
        }
    }
}
