using AIMS.DataAccess.Interface;
using AIMS.Entities;
using AIMS.Facade.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Implementation
{
    public class ProductBatchFacade : IProductBatchFacade
    {
        private readonly IProductBatchRepository _productBatchRepository;

        public ProductBatchFacade(IProductBatchRepository productBatchRepository)
        {
            _productBatchRepository = productBatchRepository;
        }

        public async Task<List<ProductBatch>> GetProductBatchesAsync()
        {
            return await _productBatchRepository.GetAllAsync();
        }

        public async Task<ProductBatch?> GetProductBatchByIdAsync(long productBatchId)
        {
            return await _productBatchRepository.GetByIdAsync(productBatchId);
        }

        public async Task AddProductBatchAsync(ProductBatch productBatch)
        {
            ArgumentNullException.ThrowIfNull(productBatch);
            await _productBatchRepository.AddAsync(productBatch);
        }

        public async Task<bool> UpdateProductBatchAsync(ProductBatch productBatch)
        {
            ArgumentNullException.ThrowIfNull(productBatch);
            var existing = await _productBatchRepository.GetByIdAsync(productBatch.ProductBatchId);
            if (existing == null)
                return false;

            await _productBatchRepository.UpdateAsync(productBatch);
            return true;
        }

        public async Task<bool> DeleteProductBatchAsync(long productBatchId)
        {
            var existing = await _productBatchRepository.GetByIdAsync(productBatchId);
            if (existing == null)
                return false;

            await _productBatchRepository.DeleteAsync(productBatchId);
            return true;
        }
    }
}
