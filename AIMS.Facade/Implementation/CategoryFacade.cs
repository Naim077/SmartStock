using AIMS.Entities;
using AIMS.DataAccess.Interface;
using AIMS.Facade.Interface;

namespace AIMS.Facade.Implementation
{
    public sealed class CategoryFacade : ICategoryFacade
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;

        public CategoryFacade(ICategoryRepository categoryRepository, IProductRepository productRepository)
        {
            ArgumentNullException.ThrowIfNull(categoryRepository);
            ArgumentNullException.ThrowIfNull(productRepository);
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }

        public Task<List<Category>> GetCategoriesAsync(CancellationToken cancellationToken = default)
        {
            return _categoryRepository.GetAllAsync(cancellationToken);
        }

        public Task<Category?> GetCategoryByIdAsync(long categoryId, CancellationToken cancellationToken = default)
        {
            return _categoryRepository.GetByIdAsync(categoryId, cancellationToken);
        }

        public Task<List<Category>> GetCategoriesPageAsync(int skip, int take, CancellationToken cancellationToken = default)
        {
            return _categoryRepository.GetPageAsync(skip, take, cancellationToken);
        }

        public async Task AddCategoryAsync(Category category, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(category);
            CatalogueValidation.NewId(category.CategoryId, nameof(category.CategoryId));
            await ValidateAsync(category, cancellationToken);
            await _categoryRepository.AddAsync(category, cancellationToken);
        }

        public async Task<bool> UpdateCategoryAsync(Category category, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(category);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(category.CategoryId);
            CatalogueValidation.RowVersion(category.RowVersion);
            if (await _categoryRepository.GetByIdAsync(category.CategoryId, cancellationToken) is null)
                return false;

            await ValidateAsync(category, cancellationToken);
            return await _categoryRepository.UpdateAsync(category, cancellationToken);
        }

        public async Task<bool> DeleteCategoryAsync(long categoryId, byte[] rowVersion, CancellationToken cancellationToken = default)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(categoryId);
            CatalogueValidation.RowVersion(rowVersion);
            if (await _categoryRepository.GetByIdAsync(categoryId, cancellationToken) is null)
                return false;

            if (await _categoryRepository.AnyAsync(category => category.ParentCategoryId == categoryId, cancellationToken))
                throw new InvalidOperationException("A category with child categories cannot be deleted.");
            if (await _productRepository.AnyAsync(product => product.CategoryId == categoryId, cancellationToken))
                throw new InvalidOperationException("A category with products, including archived products, cannot be deleted.");

            return await _categoryRepository.DeleteAsync(categoryId, rowVersion, cancellationToken);
        }

        private async Task ValidateAsync(Category category, CancellationToken cancellationToken)
        {
            category.Name = CatalogueValidation.RequiredText(category.Name, 120, nameof(category.Name));
            var parentCategoryId = category.ParentCategoryId;
            var visited = new HashSet<long>();
            while (parentCategoryId is long ancestorId)
            {
                ArgumentOutOfRangeException.ThrowIfNegativeOrZero(ancestorId, nameof(category.ParentCategoryId));
                if (ancestorId == category.CategoryId || !visited.Add(ancestorId))
                    throw new InvalidOperationException("A category cannot be its own ancestor or belong to a cyclic category hierarchy.");
                var ancestor = await _categoryRepository.GetByIdAsync(ancestorId, cancellationToken)
                    ?? throw new ArgumentException("The parent category hierarchy contains a missing category.", nameof(category.ParentCategoryId));
                parentCategoryId = ancestor.ParentCategoryId;
            }

            var normalizedName = category.Name.ToUpperInvariant();
            if (await _categoryRepository.AnyAsync(other => other.CategoryId != category.CategoryId
                && other.ParentCategoryId == category.ParentCategoryId
                && other.NormalizedName == normalizedName, cancellationToken))
                throw new ArgumentException("A category with this normalized name already exists under the same parent.", nameof(category.Name));
        }
    }
}
