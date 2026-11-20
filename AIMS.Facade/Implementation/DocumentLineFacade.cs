using AIMS.DataAccess.Interface;
using AIMS.Entities;
using AIMS.Facade.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Implementation
{
    public class DocumentLineFacade : IDocumentLineFacade
    {
        private readonly IDocumentLineRepository _documentLineRepository;

        public DocumentLineFacade(IDocumentLineRepository documentLineRepository)
        {
            _documentLineRepository = documentLineRepository;
        }

        public async Task<List<DocumentLine>> GetDocumentLinesAsync()
        {
            return await _documentLineRepository.GetAllAsync();
        }

        public async Task<DocumentLine?> GetDocumentLineByIdAsync(long lineId)
        {
            return await _documentLineRepository.GetByIdAsync(lineId);
        }

        public async Task AddDocumentLineAsync(DocumentLine documentLine)
        {
            ArgumentNullException.ThrowIfNull(documentLine);
            await _documentLineRepository.AddAsync(documentLine);
        }

        public async Task<bool> UpdateDocumentLineAsync(DocumentLine documentLine)
        {
            ArgumentNullException.ThrowIfNull(documentLine);
            var existing = await _documentLineRepository.GetByIdAsync(documentLine.LineId);
            if (existing == null)
                return false;

            await _documentLineRepository.UpdateAsync(documentLine);
            return true;
        }

        public async Task<bool> DeleteDocumentLineAsync(long lineId)
        {
            var existing = await _documentLineRepository.GetByIdAsync(lineId);
            if (existing == null)
                return false;

            await _documentLineRepository.DeleteAsync(lineId);
            return true;
        }
    }
}
