using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Interface
{
    public interface ISaleFacade
    {
        Task<List<Sale>> GetSalesAsync();
        Task<Sale?> GetSaleByIdAsync(long documentId);
        Task AddSaleAsync(Sale sale);
        Task<bool> UpdateSaleAsync(Sale sale);
        Task<bool> DeleteSaleAsync(long documentId);
    }
}
