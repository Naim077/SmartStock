using AIMS.Entities;

namespace AIMS.Facade.Interface
{
    /// <summary>Detached queries and single-record catalogue editing for Customer.</summary>
    /// <remarks>Cross-record validation is preflight only, not a transaction or authorization boundary.</remarks>
    public interface ICustomerFacade
    {
        Task<List<Customer>> GetCustomersAsync(CancellationToken cancellationToken = default);
        Task<Customer?> GetCustomerByIdAsync(long customerId, CancellationToken cancellationToken = default);
        Task<List<Customer>> GetCustomersPageAsync(int skip, int take, CancellationToken cancellationToken = default);

        /// <summary>Adds a customer. A Walk-in customer must remain active with zero credit.</summary>
        Task AddCustomerAsync(Customer customer, CancellationToken cancellationToken = default);

        /// <summary>Updates a customer without changing Walk-in designation, deactivating Walk-in, or editing its credit.</summary>
        /// <remarks>Requires the caller's original eight-byte RowVersion; returns false when missing. Concurrency errors propagate.</remarks>
        Task<bool> UpdateCustomerAsync(Customer customer, CancellationToken cancellationToken = default);

        /// <summary>Archives a non-Walk-in customer via an update, preserving history; returns false when missing.</summary>
        /// <remarks>Requires the caller's original eight-byte RowVersion. Concurrency errors propagate.</remarks>
        Task<bool> DeleteCustomerAsync(long customerId, byte[] rowVersion, CancellationToken cancellationToken = default);
    }
}
