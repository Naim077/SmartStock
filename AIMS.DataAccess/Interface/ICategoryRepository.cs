using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Interface
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<Category?> GetByIdAsync(long categoryId, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(long categoryId, byte[] rowVersion, CancellationToken cancellationToken = default);
    }
}
