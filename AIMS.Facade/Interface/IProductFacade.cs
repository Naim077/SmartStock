using AIMS.Entities;

namespace AIMS.Facade.Interface
{
    /// <summary>Detached queries and single-record catalogue editing for Product.</summary>
    /// <remarks>Cross-record validation is preflight only, not a transaction or authorization boundary.</remarks>
    public interface IProductFacade
    {
        Task<List<Product>> GetProductsAsync(CancellationToken cancellationToken = default);
        Task<Product?> GetProductByIdAsync(long productId, CancellationToken cancellationToken = default);
        Task<List<Product>> GetProductsPageAsync(int skip, int take, CancellationToken cancellationToken = default);

        /// <summary>Adds a product without a caller-supplied database-generated identifier.</summary>
        Task AddProductAsync(Product product, CancellationToken cancellationToken = default);

        /// <summary>Updates catalogue fields, preserving activation time and freezing unit, scale and expiry mode after use.</summary>
        /// <remarks>Requires the caller's original eight-byte RowVersion; returns false when missing. Concurrency errors propagate.</remarks>
        Task<bool> UpdateProductAsync(Product product, CancellationToken cancellationToken = default);

        /// <summary>Archives a product via an update, preserving history; returns false when missing.</summary>
        /// <remarks>Requires the caller's original eight-byte RowVersion. Concurrency errors propagate.</remarks>
        Task<bool> DeleteProductAsync(long productId, byte[] rowVersion, CancellationToken cancellationToken = default);
    }
}
