using AIMS.DataAccess.Interface;
using AIMS.Entities;
using AIMS.Facade.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Implementation
{
    public class StockMovementFacade : IStockMovementFacade
    {
        private readonly IStockMovementRepository _stockMovementRepository;

        public StockMovementFacade(IStockMovementRepository stockMovementRepository)
        {
            _stockMovementRepository = stockMovementRepository;
        }

        public async Task<List<StockMovement>> GetStockMovementsAsync()
        {
            return await _stockMovementRepository.GetAllAsync();
        }

        public async Task<StockMovement?> GetStockMovementByIdAsync(long stockMovementId)
        {
            return await _stockMovementRepository.GetByIdAsync(stockMovementId);
        }

        public async Task AddStockMovementAsync(StockMovement stockMovement)
        {
            ArgumentNullException.ThrowIfNull(stockMovement);
            await _stockMovementRepository.AddAsync(stockMovement);
        }

        public async Task<bool> UpdateStockMovementAsync(StockMovement stockMovement)
        {
            ArgumentNullException.ThrowIfNull(stockMovement);
            var existing = await _stockMovementRepository.GetByIdAsync(stockMovement.StockMovementId);
            if (existing == null)
                return false;

            await _stockMovementRepository.UpdateAsync(stockMovement);
            return true;
        }

        public async Task<bool> DeleteStockMovementAsync(long stockMovementId)
        {
            var existing = await _stockMovementRepository.GetByIdAsync(stockMovementId);
            if (existing == null)
                return false;

            await _stockMovementRepository.DeleteAsync(stockMovementId);
            return true;
        }
    }
}
