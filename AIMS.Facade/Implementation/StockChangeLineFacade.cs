using AIMS.DataAccess.Interface;
using AIMS.Entities;
using AIMS.Facade.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Implementation
{
    public class StockChangeLineFacade : IStockChangeLineFacade
    {
        private readonly IStockChangeLineRepository _stockChangeLineRepository;

        public StockChangeLineFacade(IStockChangeLineRepository stockChangeLineRepository)
        {
            _stockChangeLineRepository = stockChangeLineRepository;
        }

        public async Task<List<StockChangeLine>> GetStockChangeLinesAsync()
        {
            return await _stockChangeLineRepository.GetAllAsync();
        }

        public async Task<StockChangeLine?> GetStockChangeLineByIdAsync(long lineId)
        {
            return await _stockChangeLineRepository.GetByIdAsync(lineId);
        }

        public async Task AddStockChangeLineAsync(StockChangeLine stockChangeLine)
        {
            ArgumentNullException.ThrowIfNull(stockChangeLine);
            await _stockChangeLineRepository.AddAsync(stockChangeLine);
        }

        public async Task<bool> UpdateStockChangeLineAsync(StockChangeLine stockChangeLine)
        {
            ArgumentNullException.ThrowIfNull(stockChangeLine);
            var existing = await _stockChangeLineRepository.GetByIdAsync(stockChangeLine.LineId);
            if (existing == null)
                return false;

            await _stockChangeLineRepository.UpdateAsync(stockChangeLine);
            return true;
        }

        public async Task<bool> DeleteStockChangeLineAsync(long lineId)
        {
            var existing = await _stockChangeLineRepository.GetByIdAsync(lineId);
            if (existing == null)
                return false;

            await _stockChangeLineRepository.DeleteAsync(lineId);
            return true;
        }
    }
}
