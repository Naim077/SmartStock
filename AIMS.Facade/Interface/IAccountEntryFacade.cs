using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Interface
{
    public interface IAccountEntryFacade
    {
        Task<List<AccountEntry>> GetAccountEntriesAsync();
        Task<AccountEntry?> GetAccountEntryByIdAsync(long accountEntryId);
        Task AddAccountEntryAsync(AccountEntry accountEntry);
        Task<bool> UpdateAccountEntryAsync(AccountEntry accountEntry);
        Task<bool> DeleteAccountEntryAsync(long accountEntryId);
    }
}
