using AIMS.Entities;
using AIMS.DataAccess.Interface;
using AIMS.Facade.Interface;

namespace AIMS.Facade.Implementation
{
    public sealed class SalesReturnLineFacade : ISalesReturnLineFacade
    {
        private readonly ISalesReturnLineRepository _salesReturnLineRepository;

        public SalesReturnLineFacade(ISalesReturnLineRepository salesReturnLineRepository)
        {
            ArgumentNullException.ThrowIfNull(salesReturnLineRepository);
            _salesReturnLineRepository = salesReturnLineRepository;
        }

        public Task<List<SalesReturnLine>> GetSalesReturnLinesAsync(CancellationToken cancellationToken = default)
        {
            return _salesReturnLineRepository.GetAllAsync(cancellationToken);
        }

        public Task<SalesReturnLine?> GetSalesReturnLineByIdAsync(long lineId, CancellationToken cancellationToken = default)
        {
            return _salesReturnLineRepository.GetByIdAsync(lineId, cancellationToken);
        }

        public Task<List<SalesReturnLine>> GetSalesReturnLinesPageAsync(int skip, int take, CancellationToken cancellationToken = default)
        {
            return _salesReturnLineRepository.GetPageAsync(skip, take, cancellationToken);
        }
    }
}
