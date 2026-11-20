using AIMS.DataAccess.Interface;
using AIMS.Entities;
using AIMS.Facade.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Implementation
{
    public class PermissionGroupFacade : IPermissionGroupFacade
    {
        private readonly IPermissionGroupRepository _permissionGroupRepository;

        public PermissionGroupFacade(IPermissionGroupRepository permissionGroupRepository)
        {
            _permissionGroupRepository = permissionGroupRepository;
        }

        public async Task<List<PermissionGroup>> GetPermissionGroupsAsync()
        {
            return await _permissionGroupRepository.GetAllAsync();
        }

        public async Task<PermissionGroup?> GetPermissionGroupByIdAsync(int id)
        {
            return await _permissionGroupRepository.GetByIdAsync(id);
        }

        public async Task AddPermissionGroupAsync(PermissionGroup permissionGroup)
        {
            ArgumentNullException.ThrowIfNull(permissionGroup);
            await _permissionGroupRepository.AddAsync(permissionGroup);
        }

        public async Task<bool> UpdatePermissionGroupAsync(PermissionGroup permissionGroup)
        {
            ArgumentNullException.ThrowIfNull(permissionGroup);
            var existing = await _permissionGroupRepository.GetByIdAsync(permissionGroup.Id);
            if (existing == null)
                return false;

            await _permissionGroupRepository.UpdateAsync(permissionGroup);
            return true;
        }

        public async Task<bool> DeletePermissionGroupAsync(int id)
        {
            var existing = await _permissionGroupRepository.GetByIdAsync(id);
            if (existing == null)
                return false;

            await _permissionGroupRepository.DeleteAsync(id);
            return true;
        }
    }
}
