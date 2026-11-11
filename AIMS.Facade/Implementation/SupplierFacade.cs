using AIMS.Entities;
using AIMS.DataAccess.Interface;
using AIMS.Facade.Interface;

namespace AIMS.Facade.Implementation
{
    public sealed class SupplierFacade : ISupplierFacade
    {
        private readonly ISupplierRepository _supplierRepository;

        public SupplierFacade(ISupplierRepository supplierRepository)
        {
            ArgumentNullException.ThrowIfNull(supplierRepository);
            _supplierRepository = supplierRepository;
        }

        public Task<List<Supplier>> GetSuppliersAsync(CancellationToken cancellationToken = default)
        {
            return _supplierRepository.GetAllAsync(cancellationToken);
        }

        public Task<Supplier?> GetSupplierByIdAsync(long supplierId, CancellationToken cancellationToken = default)
        {
            return _supplierRepository.GetByIdAsync(supplierId, cancellationToken);
        }

        public Task<List<Supplier>> GetSuppliersPageAsync(int skip, int take, CancellationToken cancellationToken = default)
        {
            return _supplierRepository.GetPageAsync(skip, take, cancellationToken);
        }

        public async Task AddSupplierAsync(Supplier supplier, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(supplier);
            CatalogueValidation.NewId(supplier.SupplierId, nameof(supplier.SupplierId));
            Validate(supplier);
            await _supplierRepository.AddAsync(supplier, cancellationToken);
        }

        public async Task<bool> UpdateSupplierAsync(Supplier supplier, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(supplier);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(supplier.SupplierId);
            CatalogueValidation.RowVersion(supplier.RowVersion);
            if (await _supplierRepository.GetByIdAsync(supplier.SupplierId, cancellationToken) is null)
                return false;

            Validate(supplier);
            return await _supplierRepository.UpdateAsync(supplier, cancellationToken);
        }

        public async Task<bool> DeleteSupplierAsync(long supplierId, byte[] rowVersion, CancellationToken cancellationToken = default)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(supplierId);
            CatalogueValidation.RowVersion(rowVersion);
            var supplier = await _supplierRepository.GetByIdAsync(supplierId, cancellationToken);
            if (supplier is null) return false;

            supplier.IsActive = false;
            supplier.RowVersion = rowVersion.ToArray();
            return await _supplierRepository.UpdateAsync(supplier, cancellationToken);
        }

        private static void Validate(Supplier supplier)
        {
            supplier.Name = CatalogueValidation.RequiredText(supplier.Name, 200, nameof(supplier.Name));
            supplier.ContactName = CatalogueValidation.OptionalText(supplier.ContactName, 120, nameof(supplier.ContactName));
            supplier.Email = CatalogueValidation.OptionalEmail(supplier.Email, nameof(supplier.Email));
            supplier.Phone = CatalogueValidation.OptionalText(supplier.Phone, 40, nameof(supplier.Phone));
            supplier.Address = CatalogueValidation.OptionalText(supplier.Address, 500, nameof(supplier.Address));
            ArgumentOutOfRangeException.ThrowIfNegative(supplier.PaymentTermsDays);
            if (supplier.ConfiguredLeadTimeDays is decimal leadTime)
                CatalogueValidation.NonNegativeDecimal(leadTime, 6, nameof(supplier.ConfiguredLeadTimeDays));
        }
    }
}
