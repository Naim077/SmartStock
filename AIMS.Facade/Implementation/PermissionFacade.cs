using AIMS.DataAccess.Interface;
using AIMS.Entities;
using AIMS.Facade.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Implementation
{
    public class PermissionFacade : IPermissionFacade
    {
        private readonly IPermissionRepository _permissionRepository;

        public PermissionFacade(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        public async Task<List<Permission>> GetPermissionsAsync()
        {
            return await _permissionRepository.GetAllAsync();
        }

        public async Task<Permission?> GetPermissionByIdAsync(int id)
        {
            return await _permissionRepository.GetByIdAsync(id);
        }

        public async Task AddPermissionAsync(Permission permission)
        {
            ArgumentNullException.ThrowIfNull(permission);
            await _permissionRepository.AddAsync(permission);
        }

        public async Task<bool> UpdatePermissionAsync(Permission permission)
        {
            ArgumentNullException.ThrowIfNull(permission);
            var existing = await _permissionRepository.GetByIdAsync(permission.Id);
            if (existing == null)
                return false;

            await _permissionRepository.UpdateAsync(permission);
            return true;
        }

        public async Task<bool> DeletePermissionAsync(int id)
        {
            var existing = await _permissionRepository.GetByIdAsync(id);
            if (existing == null)
                return false;

            await _permissionRepository.DeleteAsync(id);
            return true;
        }
    }
}
