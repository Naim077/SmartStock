using AIMS.Entities;
using AIMS.DataAccess.Interface;
using AIMS.Facade.Interface;

namespace AIMS.Facade.Implementation
{
    public sealed class SaleFacade : ISaleFacade
    {
        private readonly ISaleRepository _saleRepository;

        public SaleFacade(ISaleRepository saleRepository)
        {
            ArgumentNullException.ThrowIfNull(saleRepository);
            _saleRepository = saleRepository;
        }

        public Task<List<Sale>> GetSalesAsync(CancellationToken cancellationToken = default)
        {
            return _saleRepository.GetAllAsync(cancellationToken);
        }

        public Task<Sale?> GetSaleByIdAsync(long documentId, CancellationToken cancellationToken = default)
        {
            return _saleRepository.GetByIdAsync(documentId, cancellationToken);
        }

        public Task<List<Sale>> GetSalesPageAsync(int skip, int take, CancellationToken cancellationToken = default)
        {
            return _saleRepository.GetPageAsync(skip, take, cancellationToken);
        }
    }
}
