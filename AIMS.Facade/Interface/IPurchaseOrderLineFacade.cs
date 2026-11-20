using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Interface
{
    public interface IPurchaseOrderLineFacade
    {
        Task<List<PurchaseOrderLine>> GetPurchaseOrderLinesAsync();
        Task<PurchaseOrderLine?> GetPurchaseOrderLineByIdAsync(long lineId);
        Task AddPurchaseOrderLineAsync(PurchaseOrderLine purchaseOrderLine);
        Task<bool> UpdatePurchaseOrderLineAsync(PurchaseOrderLine purchaseOrderLine);
        Task<bool> DeletePurchaseOrderLineAsync(long lineId);
    }
}
