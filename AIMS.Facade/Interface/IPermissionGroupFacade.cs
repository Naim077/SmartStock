using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Interface
{
    public interface IPermissionGroupFacade
    {
        Task<List<PermissionGroup>> GetPermissionGroupsAsync();
        Task<PermissionGroup?> GetPermissionGroupByIdAsync(int id);
        Task AddPermissionGroupAsync(PermissionGroup permissionGroup);
        Task<bool> UpdatePermissionGroupAsync(PermissionGroup permissionGroup);
        Task<bool> DeletePermissionGroupAsync(int id);
    }
}
