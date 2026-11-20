using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Interface
{
    public interface IPermissionFacade
    {
        Task<List<Permission>> GetPermissionsAsync();
        Task<Permission?> GetPermissionByIdAsync(int id);
        Task AddPermissionAsync(Permission permission);
        Task<bool> UpdatePermissionAsync(Permission permission);
        Task<bool> DeletePermissionAsync(int id);
    }
}
