using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Interface
{
    public interface IOpeningAccountBalanceRepository
    {
        Task<List<OpeningAccountBalance>> GetAllAsync();
        Task<OpeningAccountBalance?> GetByIdAsync(long documentId);
        Task AddAsync(OpeningAccountBalance openingAccountBalance);
        Task UpdateAsync(OpeningAccountBalance openingAccountBalance);
        Task DeleteAsync(long documentId);
    }
}
