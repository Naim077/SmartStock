using AIMS.DataAccess.Interface;
using AIMS.Entities;
using AIMS.Facade.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Implementation
{
    public class SaleLineFacade : ISaleLineFacade
    {
        private readonly ISaleLineRepository _saleLineRepository;

        public SaleLineFacade(ISaleLineRepository saleLineRepository)
        {
            _saleLineRepository = saleLineRepository;
        }

        public async Task<List<SaleLine>> GetSaleLinesAsync()
        {
            return await _saleLineRepository.GetAllAsync();
        }

        public async Task<SaleLine?> GetSaleLineByIdAsync(long lineId)
        {
            return await _saleLineRepository.GetByIdAsync(lineId);
        }

        public async Task AddSaleLineAsync(SaleLine saleLine)
        {
            ArgumentNullException.ThrowIfNull(saleLine);
            await _saleLineRepository.AddAsync(saleLine);
        }

        public async Task<bool> UpdateSaleLineAsync(SaleLine saleLine)
        {
            ArgumentNullException.ThrowIfNull(saleLine);
            var existing = await _saleLineRepository.GetByIdAsync(saleLine.LineId);
            if (existing == null)
                return false;

            await _saleLineRepository.UpdateAsync(saleLine);
            return true;
        }

        public async Task<bool> DeleteSaleLineAsync(long lineId)
        {
            var existing = await _saleLineRepository.GetByIdAsync(lineId);
            if (existing == null)
                return false;

            await _saleLineRepository.DeleteAsync(lineId);
            return true;
        }
    }
}
