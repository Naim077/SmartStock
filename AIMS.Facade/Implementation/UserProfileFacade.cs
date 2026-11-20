using AIMS.DataAccess.Interface;
using AIMS.Entities;
using AIMS.Facade.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Implementation
{
    public class UserProfileFacade : IUserProfileFacade
    {
        private readonly IUserProfileRepository _userProfileRepository;

        public UserProfileFacade(IUserProfileRepository userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
        }

        public async Task<List<UserProfile>> GetUserProfilesAsync()
        {
            return await _userProfileRepository.GetAllAsync();
        }

        public async Task<UserProfile?> GetUserProfileByIdAsync(Guid userId)
        {
            return await _userProfileRepository.GetByIdAsync(userId);
        }

        public async Task AddUserProfileAsync(UserProfile userProfile)
        {
            ArgumentNullException.ThrowIfNull(userProfile);
            await _userProfileRepository.AddAsync(userProfile);
        }

        public async Task<bool> UpdateUserProfileAsync(UserProfile userProfile)
        {
            ArgumentNullException.ThrowIfNull(userProfile);
            var existing = await _userProfileRepository.GetByIdAsync(userProfile.UserId);
            if (existing == null)
                return false;

            await _userProfileRepository.UpdateAsync(userProfile);
            return true;
        }

        public async Task<bool> DeleteUserProfileAsync(Guid userId)
        {
            var existing = await _userProfileRepository.GetByIdAsync(userId);
            if (existing == null)
                return false;

            await _userProfileRepository.DeleteAsync(userId);
            return true;
        }
    }
}
