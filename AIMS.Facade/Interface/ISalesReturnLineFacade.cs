using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Interface
{
    public interface ISalesReturnLineFacade
    {
        Task<List<SalesReturnLine>> GetSalesReturnLinesAsync();
        Task<SalesReturnLine?> GetSalesReturnLineByIdAsync(long lineId);
        Task AddSalesReturnLineAsync(SalesReturnLine salesReturnLine);
        Task<bool> UpdateSalesReturnLineAsync(SalesReturnLine salesReturnLine);
        Task<bool> DeleteSalesReturnLineAsync(long lineId);
    }
}
