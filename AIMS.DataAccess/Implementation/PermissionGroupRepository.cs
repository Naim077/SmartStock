using AIMS.DataAccess.Interface;
using AIMS.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Implementation
{
    public class PermissionGroupRepository : IPermissionGroupRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PermissionGroupRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<PermissionGroup>> GetAllAsync()
        {
            return await _dbContext.PermissionGroups.ToListAsync();
        }

        public async Task<PermissionGroup?> GetByIdAsync(int id)
        {
            return await _dbContext.PermissionGroups.FindAsync(new object[] { id });
        }

        public async Task AddAsync(PermissionGroup permissionGroup)
        {
            await _dbContext.PermissionGroups.AddAsync(permissionGroup);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(PermissionGroup permissionGroup)
        {
            _dbContext.PermissionGroups.Update(permissionGroup);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var permissionGroup = await _dbContext.PermissionGroups.FindAsync(new object[] { id });
            if (permissionGroup == null)
                return;

            _dbContext.PermissionGroups.Remove(permissionGroup);
            await _dbContext.SaveChangesAsync();
        }
    }
}
