using AIMS.DataAccess.Interface;
using AIMS.Entities;
using AIMS.Facade.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Implementation
{
    public class StockChangeFacade : IStockChangeFacade
    {
        private readonly IStockChangeRepository _stockChangeRepository;

        public StockChangeFacade(IStockChangeRepository stockChangeRepository)
        {
            _stockChangeRepository = stockChangeRepository;
        }

        public async Task<List<StockChange>> GetStockChangesAsync()
        {
            return await _stockChangeRepository.GetAllAsync();
        }

        public async Task<StockChange?> GetStockChangeByIdAsync(long documentId)
        {
            return await _stockChangeRepository.GetByIdAsync(documentId);
        }

        public async Task AddStockChangeAsync(StockChange stockChange)
        {
            ArgumentNullException.ThrowIfNull(stockChange);
            await _stockChangeRepository.AddAsync(stockChange);
        }

        public async Task<bool> UpdateStockChangeAsync(StockChange stockChange)
        {
            ArgumentNullException.ThrowIfNull(stockChange);
            var existing = await _stockChangeRepository.GetByIdAsync(stockChange.DocumentId);
            if (existing == null)
                return false;

            await _stockChangeRepository.UpdateAsync(stockChange);
            return true;
        }

        public async Task<bool> DeleteStockChangeAsync(long documentId)
        {
            var existing = await _stockChangeRepository.GetByIdAsync(documentId);
            if (existing == null)
                return false;

            await _stockChangeRepository.DeleteAsync(documentId);
            return true;
        }
    }
}
