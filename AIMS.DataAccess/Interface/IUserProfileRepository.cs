using AIMS.DataAccess.Interface;
using AIMS.Entities;
namespace AIMS.DataAccess.Interface
{
    public interface IUserProfileRepository : IReadOnlyRepository<UserProfile>
    {
        Task<UserProfile?> GetByIdAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}
