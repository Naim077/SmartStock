using AIMS.DataAccess.Interface;
using AIMS.Entities;
using AIMS.Facade.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Implementation
{
    public class ProductStockBalanceFacade : IProductStockBalanceFacade
    {
        private readonly IProductStockBalanceRepository _productStockBalanceRepository;

        public ProductStockBalanceFacade(IProductStockBalanceRepository productStockBalanceRepository)
        {
            _productStockBalanceRepository = productStockBalanceRepository;
        }

        public async Task<List<ProductStockBalance>> GetProductStockBalancesAsync()
        {
            return await _productStockBalanceRepository.GetAllAsync();
        }

        public async Task<ProductStockBalance?> GetProductStockBalanceByIdAsync(long productId)
        {
            return await _productStockBalanceRepository.GetByIdAsync(productId);
        }

        public async Task AddProductStockBalanceAsync(ProductStockBalance productStockBalance)
        {
            ArgumentNullException.ThrowIfNull(productStockBalance);
            await _productStockBalanceRepository.AddAsync(productStockBalance);
        }

        public async Task<bool> UpdateProductStockBalanceAsync(ProductStockBalance productStockBalance)
        {
            ArgumentNullException.ThrowIfNull(productStockBalance);
            var existing = await _productStockBalanceRepository.GetByIdAsync(productStockBalance.ProductId);
            if (existing == null)
                return false;

            await _productStockBalanceRepository.UpdateAsync(productStockBalance);
            return true;
        }

        public async Task<bool> DeleteProductStockBalanceAsync(long productId)
        {
            var existing = await _productStockBalanceRepository.GetByIdAsync(productId);
            if (existing == null)
                return false;

            await _productStockBalanceRepository.DeleteAsync(productId);
            return true;
        }
    }
}
