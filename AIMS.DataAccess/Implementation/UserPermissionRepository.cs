using AIMS.DataAccess.Interface;
using AIMS.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Implementation
{
    public class UserPermissionRepository : IUserPermissionRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserPermissionRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<UserPermission>> GetAllAsync()
        {
            return await _dbContext.UserPermissions.ToListAsync();
        }

        public async Task<UserPermission?> GetByIdAsync(int id)
        {
            return await _dbContext.UserPermissions.FindAsync(new object[] { id });
        }

        public async Task AddAsync(UserPermission userPermission)
        {
            await _dbContext.UserPermissions.AddAsync(userPermission);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(UserPermission userPermission)
        {
            _dbContext.UserPermissions.Update(userPermission);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var userPermission = await _dbContext.UserPermissions.FindAsync(new object[] { id });
            if (userPermission == null)
                return;

            _dbContext.UserPermissions.Remove(userPermission);
            await _dbContext.SaveChangesAsync();
        }
    }
}
