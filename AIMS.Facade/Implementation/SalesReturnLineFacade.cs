using AIMS.DataAccess.Interface;
using AIMS.Entities;
using AIMS.Facade.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Implementation
{
    public class SalesReturnLineFacade : ISalesReturnLineFacade
    {
        private readonly ISalesReturnLineRepository _salesReturnLineRepository;

        public SalesReturnLineFacade(ISalesReturnLineRepository salesReturnLineRepository)
        {
            _salesReturnLineRepository = salesReturnLineRepository;
        }

        public async Task<List<SalesReturnLine>> GetSalesReturnLinesAsync()
        {
            return await _salesReturnLineRepository.GetAllAsync();
        }

        public async Task<SalesReturnLine?> GetSalesReturnLineByIdAsync(long lineId)
        {
            return await _salesReturnLineRepository.GetByIdAsync(lineId);
        }

        public async Task AddSalesReturnLineAsync(SalesReturnLine salesReturnLine)
        {
            ArgumentNullException.ThrowIfNull(salesReturnLine);
            await _salesReturnLineRepository.AddAsync(salesReturnLine);
        }

        public async Task<bool> UpdateSalesReturnLineAsync(SalesReturnLine salesReturnLine)
        {
            ArgumentNullException.ThrowIfNull(salesReturnLine);
            var existing = await _salesReturnLineRepository.GetByIdAsync(salesReturnLine.LineId);
            if (existing == null)
                return false;

            await _salesReturnLineRepository.UpdateAsync(salesReturnLine);
            return true;
        }

        public async Task<bool> DeleteSalesReturnLineAsync(long lineId)
        {
            var existing = await _salesReturnLineRepository.GetByIdAsync(lineId);
            if (existing == null)
                return false;

            await _salesReturnLineRepository.DeleteAsync(lineId);
            return true;
        }
    }
}
