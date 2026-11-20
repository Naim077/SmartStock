using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Interface
{
    public interface IPermissionGroupRepository
    {
        Task<List<PermissionGroup>> GetAllAsync();
        Task<PermissionGroup?> GetByIdAsync(int id);
        Task AddAsync(PermissionGroup permissionGroup);
        Task UpdateAsync(PermissionGroup permissionGroup);
        Task DeleteAsync(int id);
    }
}
