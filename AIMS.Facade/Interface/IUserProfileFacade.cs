using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Interface
{
    public interface IUserProfileFacade
    {
        Task<List<UserProfile>> GetUserProfilesAsync();
        Task<UserProfile?> GetUserProfileByIdAsync(Guid userId);
        Task AddUserProfileAsync(UserProfile userProfile);
        Task<bool> UpdateUserProfileAsync(UserProfile userProfile);
        Task<bool> DeleteUserProfileAsync(Guid userId);
    }
}
