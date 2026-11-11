using AIMS.Entities;
using AIMS.DataAccess.Interface;
using AIMS.Facade.Interface;

namespace AIMS.Facade.Implementation
{
    public sealed class ProductFacade : IProductFacade
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IDocumentLineRepository _documentLineRepository;
        private readonly IProductBatchRepository _productBatchRepository;
        private readonly IStockMovementRepository _stockMovementRepository;
        private readonly IProductStockBalanceRepository _productStockBalanceRepository;
        private readonly TimeProvider _timeProvider;

        public ProductFacade(
            IProductRepository productRepository,
            ICategoryRepository categoryRepository,
            ISupplierRepository supplierRepository,
            IDocumentLineRepository documentLineRepository,
            IProductBatchRepository productBatchRepository,
            IStockMovementRepository stockMovementRepository,
            IProductStockBalanceRepository productStockBalanceRepository,
            TimeProvider timeProvider)
        {
            ArgumentNullException.ThrowIfNull(productRepository);
            ArgumentNullException.ThrowIfNull(categoryRepository);
            ArgumentNullException.ThrowIfNull(supplierRepository);
            ArgumentNullException.ThrowIfNull(documentLineRepository);
            ArgumentNullException.ThrowIfNull(productBatchRepository);
            ArgumentNullException.ThrowIfNull(stockMovementRepository);
            ArgumentNullException.ThrowIfNull(productStockBalanceRepository);
            ArgumentNullException.ThrowIfNull(timeProvider);
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _supplierRepository = supplierRepository;
            _documentLineRepository = documentLineRepository;
            _productBatchRepository = productBatchRepository;
            _stockMovementRepository = stockMovementRepository;
            _productStockBalanceRepository = productStockBalanceRepository;
            _timeProvider = timeProvider;
        }

        public Task<List<Product>> GetProductsAsync(CancellationToken cancellationToken = default)
        {
            return _productRepository.GetAllAsync(cancellationToken);
        }

        public Task<Product?> GetProductByIdAsync(long productId, CancellationToken cancellationToken = default)
        {
            return _productRepository.GetByIdAsync(productId, cancellationToken);
        }

        public Task<List<Product>> GetProductsPageAsync(int skip, int take, CancellationToken cancellationToken = default)
        {
            return _productRepository.GetPageAsync(skip, take, cancellationToken);
        }

        public async Task AddProductAsync(Product product, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(product);
            CatalogueValidation.NewId(product.ProductId, nameof(product.ProductId));
            await ValidateAsync(product, cancellationToken);
            var now = _timeProvider.GetUtcNow().UtcDateTime;
            product.ActivatedUtc = now;
            product.ArchivedUtc = product.IsActive ? null : now;
            await _productRepository.AddAsync(product, cancellationToken);
        }

        public async Task<bool> UpdateProductAsync(Product product, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(product);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(product.ProductId);
            CatalogueValidation.RowVersion(product.RowVersion);
            var existing = await _productRepository.GetByIdAsync(product.ProductId, cancellationToken);
            if (existing is null) return false;

            await ValidateAsync(product, cancellationToken);
            if ((!string.Equals(product.BaseUnit, existing.BaseUnit, StringComparison.Ordinal)
                || product.QuantityScale != existing.QuantityScale
                || product.ExpiryTracked != existing.ExpiryTracked)
                && await HasUsageAsync(product.ProductId, cancellationToken))
                throw new InvalidOperationException("Base unit, quantity scale and expiry mode cannot change after document, batch or stock usage.");

            product.ActivatedUtc = existing.ActivatedUtc;
            product.ArchivedUtc = product.IsActive ? null : existing.ArchivedUtc ?? _timeProvider.GetUtcNow().UtcDateTime;
            return await _productRepository.UpdateAsync(product, cancellationToken);
        }

        public async Task<bool> DeleteProductAsync(long productId, byte[] rowVersion, CancellationToken cancellationToken = default)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(productId);
            CatalogueValidation.RowVersion(rowVersion);
            var product = await _productRepository.GetByIdAsync(productId, cancellationToken);
            if (product is null) return false;

            product.IsActive = false;
            product.ArchivedUtc ??= _timeProvider.GetUtcNow().UtcDateTime;
            product.RowVersion = rowVersion.ToArray();
            return await _productRepository.UpdateAsync(product, cancellationToken);
        }

        private async Task<bool> HasUsageAsync(long productId, CancellationToken cancellationToken)
        {
            return await _documentLineRepository.AnyAsync(line => line.ProductId == productId, cancellationToken)
                || await _productBatchRepository.AnyAsync(batch => batch.ProductId == productId, cancellationToken)
                || await _stockMovementRepository.AnyAsync(movement => movement.ProductId == productId, cancellationToken)
                || await _productStockBalanceRepository.AnyAsync(balance => balance.ProductId == productId
                    && (balance.MovementVersion != 0 || balance.LastPostingId != null
                        || balance.Quantity != 0m || balance.InventoryValue != 0m), cancellationToken);
        }

        private async Task ValidateAsync(Product product, CancellationToken cancellationToken)
        {
            product.Name = CatalogueValidation.RequiredText(product.Name, 200, nameof(product.Name));
            product.Sku = CatalogueValidation.RequiredText(product.Sku, 64, nameof(product.Sku));
            product.Barcode = CatalogueValidation.OptionalText(product.Barcode, 64, nameof(product.Barcode));
            product.BaseUnit = CatalogueValidation.RequiredText(product.BaseUnit, 20, nameof(product.BaseUnit));
            product.ImageAssetReference = CatalogueValidation.OptionalText(product.ImageAssetReference, 240, nameof(product.ImageAssetReference));
            product.ReorderMode = CatalogueValidation.RequiredText(product.ReorderMode, 6, nameof(product.ReorderMode));
            if (product.ReorderMode is not ("Manual" or "Smart"))
                throw new ArgumentException("Reorder mode must be Manual or Smart.", nameof(product.ReorderMode));
            if (product.QuantityScale > 3)
                throw new ArgumentOutOfRangeException(nameof(product.QuantityScale), "Quantity scale must be between zero and three.");

            CatalogueValidation.NonNegativeDecimal(product.SellingUnitPrice, 6, nameof(product.SellingUnitPrice));
            CatalogueValidation.NonNegativeDecimal(product.DefaultPurchaseUnitCost, 6, nameof(product.DefaultPurchaseUnitCost));
            CatalogueValidation.NonNegativeDecimal(product.ManualReorderPoint, 3, nameof(product.ManualReorderPoint));
            CatalogueValidation.NonNegativeDecimal(product.PackQuantity, 3, nameof(product.PackQuantity));
            CatalogueValidation.NonNegativeDecimal(product.MinimumOrderQuantity, 3, nameof(product.MinimumOrderQuantity));
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(product.PackQuantity);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(product.MinimumOrderQuantity);
            var increment = product.QuantityScale switch
            {
                0 => 1m,
                1 => 0.1m,
                2 => 0.01m,
                _ => 0.001m
            };
            if (product.PackQuantity % increment != 0m)
                throw new ArgumentException("Pack quantity must be a multiple of the product quantity increment.", nameof(product.PackQuantity));
            if (product.MinimumOrderQuantity % increment != 0m)
                throw new ArgumentException("Minimum order quantity must be a multiple of the product quantity increment.", nameof(product.MinimumOrderQuantity));
            if (product.ManualReorderPoint % increment != 0m)
                throw new ArgumentException("Manual reorder point must be a multiple of the product quantity increment.", nameof(product.ManualReorderPoint));

            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(product.CategoryId);
            if (!await _categoryRepository.AnyAsync(category => category.CategoryId == product.CategoryId, cancellationToken))
                throw new ArgumentException("The product category does not exist.", nameof(product.CategoryId));
            if (product.PreferredSupplierId is long supplierId)
            {
                ArgumentOutOfRangeException.ThrowIfNegativeOrZero(supplierId, nameof(product.PreferredSupplierId));
                if (!await _supplierRepository.AnyAsync(supplier => supplier.SupplierId == supplierId && supplier.IsActive, cancellationToken))
                    throw new ArgumentException("The preferred supplier must exist and be active.", nameof(product.PreferredSupplierId));
            }

            var normalizedSku = product.Sku.ToUpperInvariant();
            if (await _productRepository.AnyAsync(other => other.ProductId != product.ProductId
                && other.NormalizedSku == normalizedSku, cancellationToken))
                throw new ArgumentException("A product with this normalized SKU already exists, including archived products.", nameof(product.Sku));
            var normalizedBarcode = product.Barcode?.ToUpperInvariant();
            if (normalizedBarcode is not null && await _productRepository.AnyAsync(other => other.ProductId != product.ProductId
                && other.NormalizedBarcode == normalizedBarcode, cancellationToken))
                throw new ArgumentException("A product with this normalized barcode already exists, including archived products.", nameof(product.Barcode));
        }
    }
}
