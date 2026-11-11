using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Implementation
{
    public sealed class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<Category?> GetByIdAsync(long categoryId, CancellationToken cancellationToken = default)
        {
            return base.GetByIdAsync(new object[] { categoryId }, cancellationToken);
        }

        public Task<bool> DeleteAsync(long categoryId, byte[] rowVersion, CancellationToken cancellationToken = default)
        {
            return base.DeleteAsync(new object[] { categoryId }, rowVersion, cancellationToken);
        }
    }
}
