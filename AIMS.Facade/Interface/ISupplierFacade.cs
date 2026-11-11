using AIMS.Entities;

namespace AIMS.Facade.Interface
{
    /// <summary>Detached queries and single-record catalogue editing for Supplier.</summary>
    /// <remarks>Cross-record validation is preflight only, not a transaction or authorization boundary.</remarks>
    public interface ISupplierFacade
    {
        Task<List<Supplier>> GetSuppliersAsync(CancellationToken cancellationToken = default);
        Task<Supplier?> GetSupplierByIdAsync(long supplierId, CancellationToken cancellationToken = default);
        Task<List<Supplier>> GetSuppliersPageAsync(int skip, int take, CancellationToken cancellationToken = default);

        /// <summary>Adds a supplier without a caller-supplied database-generated identifier.</summary>
        Task AddSupplierAsync(Supplier supplier, CancellationToken cancellationToken = default);

        /// <summary>Updates a supplier; returns false if the record no longer exists.</summary>
        /// <remarks>Requires the caller's original eight-byte RowVersion; returns false when missing. Concurrency errors propagate.</remarks>
        Task<bool> UpdateSupplierAsync(Supplier supplier, CancellationToken cancellationToken = default);

        /// <summary>Archives a supplier via an update, preserving history; returns false when missing.</summary>
        /// <remarks>Requires the caller's original eight-byte RowVersion. Concurrency errors propagate.</remarks>
        Task<bool> DeleteSupplierAsync(long supplierId, byte[] rowVersion, CancellationToken cancellationToken = default);
    }
}
