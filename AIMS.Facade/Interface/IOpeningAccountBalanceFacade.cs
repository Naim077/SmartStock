using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Interface
{
    public interface IOpeningAccountBalanceFacade
    {
        Task<List<OpeningAccountBalance>> GetOpeningAccountBalancesAsync();
        Task<OpeningAccountBalance?> GetOpeningAccountBalanceByIdAsync(long documentId);
        Task AddOpeningAccountBalanceAsync(OpeningAccountBalance openingAccountBalance);
        Task<bool> UpdateOpeningAccountBalanceAsync(OpeningAccountBalance openingAccountBalance);
        Task<bool> DeleteOpeningAccountBalanceAsync(long documentId);
    }
}
