using AIMS.Entities;
using AIMS.DataAccess.Interface;
using AIMS.Facade.Interface;

namespace AIMS.Facade.Implementation
{
    public sealed class AccountEntryFacade : IAccountEntryFacade
    {
        private readonly IAccountEntryRepository _accountEntryRepository;

        public AccountEntryFacade(IAccountEntryRepository accountEntryRepository)
        {
            ArgumentNullException.ThrowIfNull(accountEntryRepository);
            _accountEntryRepository = accountEntryRepository;
        }

        public Task<List<AccountEntry>> GetAccountEntriesAsync(CancellationToken cancellationToken = default)
        {
            return _accountEntryRepository.GetAllAsync(cancellationToken);
        }

        public Task<AccountEntry?> GetAccountEntryByIdAsync(long accountEntryId, CancellationToken cancellationToken = default)
        {
            return _accountEntryRepository.GetByIdAsync(accountEntryId, cancellationToken);
        }

        public Task<List<AccountEntry>> GetAccountEntriesPageAsync(int skip, int take, CancellationToken cancellationToken = default)
        {
            return _accountEntryRepository.GetPageAsync(skip, take, cancellationToken);
        }
    }
}
