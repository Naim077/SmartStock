using AIMS.DataAccess.Interface;
using AIMS.Entities;
using AIMS.Facade.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Implementation
{
    public class StockBalanceFacade : IStockBalanceFacade
    {
        private readonly IStockBalanceRepository _stockBalanceRepository;

        public StockBalanceFacade(IStockBalanceRepository stockBalanceRepository)
        {
            _stockBalanceRepository = stockBalanceRepository;
        }

        public async Task<List<StockBalance>> GetStockBalancesAsync()
        {
            return await _stockBalanceRepository.GetAllAsync();
        }

        public async Task<StockBalance?> GetStockBalanceByIdAsync(long productBatchId, string bucket)
        {
            return await _stockBalanceRepository.GetByIdAsync(productBatchId, bucket);
        }

        public async Task AddStockBalanceAsync(StockBalance stockBalance)
        {
            ArgumentNullException.ThrowIfNull(stockBalance);
            await _stockBalanceRepository.AddAsync(stockBalance);
        }

        public async Task<bool> UpdateStockBalanceAsync(StockBalance stockBalance)
        {
            ArgumentNullException.ThrowIfNull(stockBalance);
            var existing = await _stockBalanceRepository.GetByIdAsync(stockBalance.ProductBatchId, stockBalance.Bucket);
            if (existing == null)
                return false;

            await _stockBalanceRepository.UpdateAsync(stockBalance);
            return true;
        }

        public async Task<bool> DeleteStockBalanceAsync(long productBatchId, string bucket)
        {
            var existing = await _stockBalanceRepository.GetByIdAsync(productBatchId, bucket);
            if (existing == null)
                return false;

            await _stockBalanceRepository.DeleteAsync(productBatchId, bucket);
            return true;
        }
    }
}
