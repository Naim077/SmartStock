using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Interface
{
    public interface IPurchaseOrderLineRepository
    {
        Task<List<PurchaseOrderLine>> GetAllAsync();
        Task<PurchaseOrderLine?> GetByIdAsync(long lineId);
        Task AddAsync(PurchaseOrderLine purchaseOrderLine);
        Task UpdateAsync(PurchaseOrderLine purchaseOrderLine);
        Task DeleteAsync(long lineId);
    }
}
