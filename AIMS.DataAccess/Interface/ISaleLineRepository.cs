using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Interface
{
    public interface ISaleLineRepository
    {
        Task<List<SaleLine>> GetAllAsync();
        Task<SaleLine?> GetByIdAsync(long lineId);
        Task AddAsync(SaleLine saleLine);
        Task UpdateAsync(SaleLine saleLine);
        Task DeleteAsync(long lineId);
    }
}
