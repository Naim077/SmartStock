using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Interface
{
    public interface IUserPermissionRepository
    {
        Task<List<UserPermission>> GetAllAsync();
        Task<UserPermission?> GetByIdAsync(int id);
        Task AddAsync(UserPermission userPermission);
        Task UpdateAsync(UserPermission userPermission);
        Task DeleteAsync(int id);
    }
}
