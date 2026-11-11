using AIMS.Entities;
using AIMS.DataAccess.Interface;
using AIMS.Facade.Interface;

namespace AIMS.Facade.Implementation
{
    public sealed class AccountAllocationFacade : IAccountAllocationFacade
    {
        private readonly IAccountAllocationRepository _accountAllocationRepository;

        public AccountAllocationFacade(IAccountAllocationRepository accountAllocationRepository)
        {
            ArgumentNullException.ThrowIfNull(accountAllocationRepository);
            _accountAllocationRepository = accountAllocationRepository;
        }

        public Task<List<AccountAllocation>> GetAccountAllocationsAsync(CancellationToken cancellationToken = default)
        {
            return _accountAllocationRepository.GetAllAsync(cancellationToken);
        }

        public Task<AccountAllocation?> GetAccountAllocationByIdAsync(long allocationId, CancellationToken cancellationToken = default)
        {
            return _accountAllocationRepository.GetByIdAsync(allocationId, cancellationToken);
        }

        public Task<List<AccountAllocation>> GetAccountAllocationsPageAsync(int skip, int take, CancellationToken cancellationToken = default)
        {
            return _accountAllocationRepository.GetPageAsync(skip, take, cancellationToken);
        }
    }
}
