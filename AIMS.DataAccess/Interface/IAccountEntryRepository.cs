using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Interface
{
    public interface IAccountEntryRepository
    {
        Task<List<AccountEntry>> GetAllAsync();
        Task<AccountEntry?> GetByIdAsync(long accountEntryId);
        Task AddAsync(AccountEntry accountEntry);
        Task UpdateAsync(AccountEntry accountEntry);
        Task DeleteAsync(long accountEntryId);
    }
}
