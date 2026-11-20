using AIMS.DataAccess.Interface;
using AIMS.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Implementation
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserProfileRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<UserProfile>> GetAllAsync()
        {
            return await _dbContext.UserProfiles.ToListAsync();
        }

        public async Task<UserProfile?> GetByIdAsync(Guid userId)
        {
            return await _dbContext.UserProfiles.FindAsync(new object[] { userId });
        }

        public async Task AddAsync(UserProfile userProfile)
        {
            await _dbContext.UserProfiles.AddAsync(userProfile);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(UserProfile userProfile)
        {
            _dbContext.UserProfiles.Update(userProfile);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid userId)
        {
            var userProfile = await _dbContext.UserProfiles.FindAsync(new object[] { userId });
            if (userProfile == null)
                return;

            _dbContext.UserProfiles.Remove(userProfile);
            await _dbContext.SaveChangesAsync();
        }
    }
}
