using AIMS.DataAccess.Interface;
using AIMS.Entities;
using AIMS.Facade.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Implementation
{
    public class SupplierFacade : ISupplierFacade
    {
        private readonly ISupplierRepository _supplierRepository;

        public SupplierFacade(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task<List<Supplier>> GetSuppliersAsync()
        {
            return await _supplierRepository.GetAllAsync();
        }

        public async Task<Supplier?> GetSupplierByIdAsync(long supplierId)
        {
            return await _supplierRepository.GetByIdAsync(supplierId);
        }

        public async Task AddSupplierAsync(Supplier supplier)
        {
            ArgumentNullException.ThrowIfNull(supplier);
            await _supplierRepository.AddAsync(supplier);
        }

        public async Task<bool> UpdateSupplierAsync(Supplier supplier)
        {
            ArgumentNullException.ThrowIfNull(supplier);
            var existing = await _supplierRepository.GetByIdAsync(supplier.SupplierId);
            if (existing == null)
                return false;

            await _supplierRepository.UpdateAsync(supplier);
            return true;
        }

        public async Task<bool> DeleteSupplierAsync(long supplierId)
        {
            var existing = await _supplierRepository.GetByIdAsync(supplierId);
            if (existing == null)
                return false;

            await _supplierRepository.DeleteAsync(supplierId);
            return true;
        }
    }
}
