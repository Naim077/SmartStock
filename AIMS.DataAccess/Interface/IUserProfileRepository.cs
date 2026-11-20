using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Interface
{
    public interface IUserProfileRepository
    {
        Task<List<UserProfile>> GetAllAsync();
        Task<UserProfile?> GetByIdAsync(Guid userId);
        Task AddAsync(UserProfile userProfile);
        Task UpdateAsync(UserProfile userProfile);
        Task DeleteAsync(Guid userId);
    }
}
