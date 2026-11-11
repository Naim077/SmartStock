using AIMS.Entities;
using AIMS.DataAccess.Interface;
using AIMS.Facade.Interface;

namespace AIMS.Facade.Implementation
{
    public sealed class OpeningAccountBalanceFacade : IOpeningAccountBalanceFacade
    {
        private readonly IOpeningAccountBalanceRepository _openingAccountBalanceRepository;

        public OpeningAccountBalanceFacade(IOpeningAccountBalanceRepository openingAccountBalanceRepository)
        {
            ArgumentNullException.ThrowIfNull(openingAccountBalanceRepository);
            _openingAccountBalanceRepository = openingAccountBalanceRepository;
        }

        public Task<List<OpeningAccountBalance>> GetOpeningAccountBalancesAsync(CancellationToken cancellationToken = default)
        {
            return _openingAccountBalanceRepository.GetAllAsync(cancellationToken);
        }

        public Task<OpeningAccountBalance?> GetOpeningAccountBalanceByIdAsync(long documentId, CancellationToken cancellationToken = default)
        {
            return _openingAccountBalanceRepository.GetByIdAsync(documentId, cancellationToken);
        }

        public Task<List<OpeningAccountBalance>> GetOpeningAccountBalancesPageAsync(int skip, int take, CancellationToken cancellationToken = default)
        {
            return _openingAccountBalanceRepository.GetPageAsync(skip, take, cancellationToken);
        }
    }
}
