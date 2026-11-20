using AIMS.DataAccess.Interface;
using AIMS.Entities;
using AIMS.Facade.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Implementation
{
    public class AccountEntryFacade : IAccountEntryFacade
    {
        private readonly IAccountEntryRepository _accountEntryRepository;

        public AccountEntryFacade(IAccountEntryRepository accountEntryRepository)
        {
            _accountEntryRepository = accountEntryRepository;
        }

        public async Task<List<AccountEntry>> GetAccountEntriesAsync()
        {
            return await _accountEntryRepository.GetAllAsync();
        }

        public async Task<AccountEntry?> GetAccountEntryByIdAsync(long accountEntryId)
        {
            return await _accountEntryRepository.GetByIdAsync(accountEntryId);
        }

        public async Task AddAccountEntryAsync(AccountEntry accountEntry)
        {
            ArgumentNullException.ThrowIfNull(accountEntry);
            await _accountEntryRepository.AddAsync(accountEntry);
        }

        public async Task<bool> UpdateAccountEntryAsync(AccountEntry accountEntry)
        {
            ArgumentNullException.ThrowIfNull(accountEntry);
            var existing = await _accountEntryRepository.GetByIdAsync(accountEntry.AccountEntryId);
            if (existing == null)
                return false;

            await _accountEntryRepository.UpdateAsync(accountEntry);
            return true;
        }

        public async Task<bool> DeleteAccountEntryAsync(long accountEntryId)
        {
            var existing = await _accountEntryRepository.GetByIdAsync(accountEntryId);
            if (existing == null)
                return false;

            await _accountEntryRepository.DeleteAsync(accountEntryId);
            return true;
        }
    }
}
