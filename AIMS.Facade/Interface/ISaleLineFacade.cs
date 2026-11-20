using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Interface
{
    public interface ISaleLineFacade
    {
        Task<List<SaleLine>> GetSaleLinesAsync();
        Task<SaleLine?> GetSaleLineByIdAsync(long lineId);
        Task AddSaleLineAsync(SaleLine saleLine);
        Task<bool> UpdateSaleLineAsync(SaleLine saleLine);
        Task<bool> DeleteSaleLineAsync(long lineId);
    }
}
