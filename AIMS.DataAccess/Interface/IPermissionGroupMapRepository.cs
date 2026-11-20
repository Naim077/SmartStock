using AIMS.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.DataAccess.Interface
{
    public interface IPermissionGroupMapRepository
    {
        Task<List<PermissionGroupMap>> GetAllAsync();
        Task<PermissionGroupMap?> GetByIdAsync(int id);
        Task AddAsync(PermissionGroupMap permissionGroupMap);
        Task UpdateAsync(PermissionGroupMap permissionGroupMap);
        Task DeleteAsync(int id);
    }
}
