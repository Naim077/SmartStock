using AIMS.DataAccess.Interface;
using AIMS.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Implementation
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PermissionRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Permission>> GetAllAsync()
        {
            return await _dbContext.Permissions.ToListAsync();
        }

        public async Task<Permission?> GetByIdAsync(int id)
        {
            return await _dbContext.Permissions.FindAsync(new object[] { id });
        }

        public async Task AddAsync(Permission permission)
        {
            await _dbContext.Permissions.AddAsync(permission);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Permission permission)
        {
            _dbContext.Permissions.Update(permission);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var permission = await _dbContext.Permissions.FindAsync(new object[] { id });
            if (permission == null)
                return;

            _dbContext.Permissions.Remove(permission);
            await _dbContext.SaveChangesAsync();
        }
    }
}
