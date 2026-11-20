using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Interface
{
    public interface ISalesReturnRepository
    {
        Task<List<SalesReturn>> GetAllAsync();
        Task<SalesReturn?> GetByIdAsync(long documentId);
        Task AddAsync(SalesReturn salesReturn);
        Task UpdateAsync(SalesReturn salesReturn);
        Task DeleteAsync(long documentId);
    }
}
