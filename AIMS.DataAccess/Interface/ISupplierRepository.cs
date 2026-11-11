using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Interface
{
    public interface ISupplierRepository : IRepository<Supplier>
    {
        Task<Supplier?> GetByIdAsync(long supplierId, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(long supplierId, byte[] rowVersion, CancellationToken cancellationToken = default);
    }
}
