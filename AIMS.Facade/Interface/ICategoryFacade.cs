using AIMS.Entities;

namespace AIMS.Facade.Interface
{
    /// <summary>Detached queries and single-record catalogue editing for Category.</summary>
    /// <remarks>Cross-record validation is preflight only, not a transaction or authorization boundary.</remarks>
    public interface ICategoryFacade
    {
        Task<List<Category>> GetCategoriesAsync(CancellationToken cancellationToken = default);
        Task<Category?> GetCategoryByIdAsync(long categoryId, CancellationToken cancellationToken = default);
        Task<List<Category>> GetCategoriesPageAsync(int skip, int take, CancellationToken cancellationToken = default);

        /// <summary>Adds a category without a caller-supplied database-generated identifier.</summary>
        Task AddCategoryAsync(Category category, CancellationToken cancellationToken = default);

        /// <summary>Updates a category; returns false if the record no longer exists.</summary>
        /// <remarks>Requires the caller's original eight-byte RowVersion; returns false when missing. Concurrency errors propagate.</remarks>
        Task<bool> UpdateCategoryAsync(Category category, CancellationToken cancellationToken = default);

        /// <summary>Deletes only an empty category with no children or products; returns false when missing.</summary>
        /// <remarks>Requires the caller's original eight-byte RowVersion. Concurrency errors propagate.</remarks>
        Task<bool> DeleteCategoryAsync(long categoryId, byte[] rowVersion, CancellationToken cancellationToken = default);
    }
}
