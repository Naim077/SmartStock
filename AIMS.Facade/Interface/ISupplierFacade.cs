using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Interface
{
    public interface ISupplierFacade
    {
        Task<List<Supplier>> GetSuppliersAsync();
        Task<Supplier?> GetSupplierByIdAsync(long supplierId);
        Task AddSupplierAsync(Supplier supplier);
        Task<bool> UpdateSupplierAsync(Supplier supplier);
        Task<bool> DeleteSupplierAsync(long supplierId);
    }
}
