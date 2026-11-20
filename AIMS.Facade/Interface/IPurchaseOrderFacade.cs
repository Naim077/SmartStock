using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Interface
{
    public interface IPurchaseOrderFacade
    {
        Task<List<PurchaseOrder>> GetPurchaseOrdersAsync();
        Task<PurchaseOrder?> GetPurchaseOrderByIdAsync(long documentId);
        Task AddPurchaseOrderAsync(PurchaseOrder purchaseOrder);
        Task<bool> UpdatePurchaseOrderAsync(PurchaseOrder purchaseOrder);
        Task<bool> DeletePurchaseOrderAsync(long documentId);
    }
}
