using AIMS.Entities;
using AIMS.DataAccess.Interface;
using AIMS.Facade.Interface;

namespace AIMS.Facade.Implementation
{
    public sealed class UserProfileFacade : IUserProfileFacade
    {
        private readonly IUserProfileRepository _userProfileRepository;

        public UserProfileFacade(IUserProfileRepository userProfileRepository)
        {
            ArgumentNullException.ThrowIfNull(userProfileRepository);
            _userProfileRepository = userProfileRepository;
        }

        public Task<List<UserProfile>> GetUserProfilesAsync(CancellationToken cancellationToken = default)
        {
            return _userProfileRepository.GetAllAsync(cancellationToken);
        }

        public Task<UserProfile?> GetUserProfileByIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return _userProfileRepository.GetByIdAsync(userId, cancellationToken);
        }

        public Task<List<UserProfile>> GetUserProfilesPageAsync(int skip, int take, CancellationToken cancellationToken = default)
        {
            return _userProfileRepository.GetPageAsync(skip, take, cancellationToken);
        }
    }
}
