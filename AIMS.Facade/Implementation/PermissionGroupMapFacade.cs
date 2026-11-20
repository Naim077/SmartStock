using AIMS.DataAccess.Interface;
using AIMS.Entities;
using AIMS.Facade.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Implementation
{
    public class PermissionGroupMapFacade : IPermissionGroupMapFacade
    {
        private readonly IPermissionGroupMapRepository _permissionGroupMapRepository;

        public PermissionGroupMapFacade(IPermissionGroupMapRepository permissionGroupMapRepository)
        {
            _permissionGroupMapRepository = permissionGroupMapRepository;
        }

        public async Task<List<PermissionGroupMap>> GetPermissionGroupMapsAsync()
        {
            return await _permissionGroupMapRepository.GetAllAsync();
        }

        public async Task<PermissionGroupMap?> GetPermissionGroupMapByIdAsync(int id)
        {
            return await _permissionGroupMapRepository.GetByIdAsync(id);
        }

        public async Task AddPermissionGroupMapAsync(PermissionGroupMap permissionGroupMap)
        {
            ArgumentNullException.ThrowIfNull(permissionGroupMap);
            await _permissionGroupMapRepository.AddAsync(permissionGroupMap);
        }

        public async Task<bool> UpdatePermissionGroupMapAsync(PermissionGroupMap permissionGroupMap)
        {
            ArgumentNullException.ThrowIfNull(permissionGroupMap);
            var existing = await _permissionGroupMapRepository.GetByIdAsync(permissionGroupMap.Id);
            if (existing == null)
                return false;

            await _permissionGroupMapRepository.UpdateAsync(permissionGroupMap);
            return true;
        }

        public async Task<bool> DeletePermissionGroupMapAsync(int id)
        {
            var existing = await _permissionGroupMapRepository.GetByIdAsync(id);
            if (existing == null)
                return false;

            await _permissionGroupMapRepository.DeleteAsync(id);
            return true;
        }
    }
}
