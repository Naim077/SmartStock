using AIMS.Entities;
using AIMS.DataAccess.Interface;
using AIMS.Facade.Interface;

namespace AIMS.Facade.Implementation
{
    public sealed class StockChangeLineFacade : IStockChangeLineFacade
    {
        private readonly IStockChangeLineRepository _stockChangeLineRepository;

        public StockChangeLineFacade(IStockChangeLineRepository stockChangeLineRepository)
        {
            ArgumentNullException.ThrowIfNull(stockChangeLineRepository);
            _stockChangeLineRepository = stockChangeLineRepository;
        }

        public Task<List<StockChangeLine>> GetStockChangeLinesAsync(CancellationToken cancellationToken = default)
        {
            return _stockChangeLineRepository.GetAllAsync(cancellationToken);
        }

        public Task<StockChangeLine?> GetStockChangeLineByIdAsync(long lineId, CancellationToken cancellationToken = default)
        {
            return _stockChangeLineRepository.GetByIdAsync(lineId, cancellationToken);
        }

        public Task<List<StockChangeLine>> GetStockChangeLinesPageAsync(int skip, int take, CancellationToken cancellationToken = default)
        {
            return _stockChangeLineRepository.GetPageAsync(skip, take, cancellationToken);
        }
    }
}
