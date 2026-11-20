using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Interface
{
    public interface ISalesReturnLineRepository
    {
        Task<List<SalesReturnLine>> GetAllAsync();
        Task<SalesReturnLine?> GetByIdAsync(long lineId);
        Task AddAsync(SalesReturnLine salesReturnLine);
        Task UpdateAsync(SalesReturnLine salesReturnLine);
        Task DeleteAsync(long lineId);
    }
}
