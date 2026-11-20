using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Interface
{
    public interface IUserPermissionFacade
    {
        Task<List<UserPermission>> GetUserPermissionsAsync();
        Task<UserPermission?> GetUserPermissionByIdAsync(int id);
        Task AddUserPermissionAsync(UserPermission userPermission);
        Task<bool> UpdateUserPermissionAsync(UserPermission userPermission);
        Task<bool> DeleteUserPermissionAsync(int id);
    }
}
