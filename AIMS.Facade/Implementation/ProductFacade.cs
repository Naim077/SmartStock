using AIMS.DataAccess.Interface;
using AIMS.Entities;
using AIMS.Facade.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Implementation
{
    public class ProductFacade : IProductFacade
    {
        private readonly IProductRepository _productRepository;

        public ProductFacade(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<Product?> GetProductByIdAsync(long productId)
        {
            return await _productRepository.GetByIdAsync(productId);
        }

        public async Task AddProductAsync(Product product)
        {
            ArgumentNullException.ThrowIfNull(product);
            await _productRepository.AddAsync(product);
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            ArgumentNullException.ThrowIfNull(product);
            var existing = await _productRepository.GetByIdAsync(product.ProductId);
            if (existing == null)
                return false;

            await _productRepository.UpdateAsync(product);
            return true;
        }

        public async Task<bool> DeleteProductAsync(long productId)
        {
            var existing = await _productRepository.GetByIdAsync(productId);
            if (existing == null)
                return false;

            await _productRepository.DeleteAsync(productId);
            return true;
        }
    }
}
