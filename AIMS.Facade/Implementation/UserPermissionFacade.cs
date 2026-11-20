using AIMS.DataAccess.Interface;
using AIMS.Entities;
using AIMS.Facade.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Implementation
{
    public class UserPermissionFacade : IUserPermissionFacade
    {
        private readonly IUserPermissionRepository _userPermissionRepository;

        public UserPermissionFacade(IUserPermissionRepository userPermissionRepository)
        {
            _userPermissionRepository = userPermissionRepository;
        }

        public async Task<List<UserPermission>> GetUserPermissionsAsync()
        {
            return await _userPermissionRepository.GetAllAsync();
        }

        public async Task<UserPermission?> GetUserPermissionByIdAsync(int id)
        {
            return await _userPermissionRepository.GetByIdAsync(id);
        }

        public async Task AddUserPermissionAsync(UserPermission userPermission)
        {
            ArgumentNullException.ThrowIfNull(userPermission);
            await _userPermissionRepository.AddAsync(userPermission);
        }

        public async Task<bool> UpdateUserPermissionAsync(UserPermission userPermission)
        {
            ArgumentNullException.ThrowIfNull(userPermission);
            var existing = await _userPermissionRepository.GetByIdAsync(userPermission.Id);
            if (existing == null)
                return false;

            await _userPermissionRepository.UpdateAsync(userPermission);
            return true;
        }

        public async Task<bool> DeleteUserPermissionAsync(int id)
        {
            var existing = await _userPermissionRepository.GetByIdAsync(id);
            if (existing == null)
                return false;

            await _userPermissionRepository.DeleteAsync(id);
            return true;
        }
    }
}
