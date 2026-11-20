using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Interface
{
    public interface IPermissionGroupMapFacade
    {
        Task<List<PermissionGroupMap>> GetPermissionGroupMapsAsync();
        Task<PermissionGroupMap?> GetPermissionGroupMapByIdAsync(int id);
        Task AddPermissionGroupMapAsync(PermissionGroupMap permissionGroupMap);
        Task<bool> UpdatePermissionGroupMapAsync(PermissionGroupMap permissionGroupMap);
        Task<bool> DeletePermissionGroupMapAsync(int id);
    }
}
