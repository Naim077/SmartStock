using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Interface
{
    public interface ISalesReturnFacade
    {
        Task<List<SalesReturn>> GetSalesReturnsAsync();
        Task<SalesReturn?> GetSalesReturnByIdAsync(long documentId);
        Task AddSalesReturnAsync(SalesReturn salesReturn);
        Task<bool> UpdateSalesReturnAsync(SalesReturn salesReturn);
        Task<bool> DeleteSalesReturnAsync(long documentId);
    }
}
