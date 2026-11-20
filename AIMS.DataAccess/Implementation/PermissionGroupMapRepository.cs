using AIMS.DataAccess.Interface;
using AIMS.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Implementation
{
    public class PermissionGroupMapRepository : IPermissionGroupMapRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PermissionGroupMapRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<PermissionGroupMap>> GetAllAsync()
        {
            return await _dbContext.PermissionGroupMaps.ToListAsync();
        }

        public async Task<PermissionGroupMap?> GetByIdAsync(int id)
        {
            return await _dbContext.PermissionGroupMaps.FindAsync(new object[] { id });
        }

        public async Task AddAsync(PermissionGroupMap permissionGroupMap)
        {
            await _dbContext.PermissionGroupMaps.AddAsync(permissionGroupMap);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(PermissionGroupMap permissionGroupMap)
        {
            _dbContext.PermissionGroupMaps.Update(permissionGroupMap);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var permissionGroupMap = await _dbContext.PermissionGroupMaps.FindAsync(new object[] { id });
            if (permissionGroupMap == null)
                return;

            _dbContext.PermissionGroupMaps.Remove(permissionGroupMap);
            await _dbContext.SaveChangesAsync();
        }
    }
}
