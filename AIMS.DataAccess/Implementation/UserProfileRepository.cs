using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Implementation
{
    public sealed class UserProfileRepository : ReadOnlyRepository<UserProfile>, IUserProfileRepository
    {
        public UserProfileRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<UserProfile?> GetByIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return base.GetByIdAsync(new object[] { userId }, cancellationToken);
        }
    }
}
