using AIMS.Entities;
using AIMS.DataAccess.Interface;
using AIMS.Facade.Interface;

namespace AIMS.Facade.Implementation
{
    public sealed class SaleLineFacade : ISaleLineFacade
    {
        private readonly ISaleLineRepository _saleLineRepository;

        public SaleLineFacade(ISaleLineRepository saleLineRepository)
        {
            ArgumentNullException.ThrowIfNull(saleLineRepository);
            _saleLineRepository = saleLineRepository;
        }

        public Task<List<SaleLine>> GetSaleLinesAsync(CancellationToken cancellationToken = default)
        {
            return _saleLineRepository.GetAllAsync(cancellationToken);
        }

        public Task<SaleLine?> GetSaleLineByIdAsync(long lineId, CancellationToken cancellationToken = default)
        {
            return _saleLineRepository.GetByIdAsync(lineId, cancellationToken);
        }

        public Task<List<SaleLine>> GetSaleLinesPageAsync(int skip, int take, CancellationToken cancellationToken = default)
        {
            return _saleLineRepository.GetPageAsync(skip, take, cancellationToken);
        }
    }
}
