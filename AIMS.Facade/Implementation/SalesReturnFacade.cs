using AIMS.Entities;
using AIMS.DataAccess.Interface;
using AIMS.Facade.Interface;

namespace AIMS.Facade.Implementation
{
    public sealed class SalesReturnFacade : ISalesReturnFacade
    {
        private readonly ISalesReturnRepository _salesReturnRepository;

        public SalesReturnFacade(ISalesReturnRepository salesReturnRepository)
        {
            ArgumentNullException.ThrowIfNull(salesReturnRepository);
            _salesReturnRepository = salesReturnRepository;
        }

        public Task<List<SalesReturn>> GetSalesReturnsAsync(CancellationToken cancellationToken = default)
        {
            return _salesReturnRepository.GetAllAsync(cancellationToken);
        }

        public Task<SalesReturn?> GetSalesReturnByIdAsync(long documentId, CancellationToken cancellationToken = default)
        {
            return _salesReturnRepository.GetByIdAsync(documentId, cancellationToken);
        }

        public Task<List<SalesReturn>> GetSalesReturnsPageAsync(int skip, int take, CancellationToken cancellationToken = default)
        {
            return _salesReturnRepository.GetPageAsync(skip, take, cancellationToken);
        }
    }
}
