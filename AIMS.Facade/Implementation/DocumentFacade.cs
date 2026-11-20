using AIMS.DataAccess.Interface;
using AIMS.Entities;
using AIMS.Facade.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Implementation
{
    public class DocumentFacade : IDocumentFacade
    {
        private readonly IDocumentRepository _documentRepository;

        public DocumentFacade(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public async Task<List<Document>> GetDocumentsAsync()
        {
            return await _documentRepository.GetAllAsync();
        }

        public async Task<Document?> GetDocumentByIdAsync(long documentId)
        {
            return await _documentRepository.GetByIdAsync(documentId);
        }

        public async Task AddDocumentAsync(Document document)
        {
            ArgumentNullException.ThrowIfNull(document);
            await _documentRepository.AddAsync(document);
        }

        public async Task<bool> UpdateDocumentAsync(Document document)
        {
            ArgumentNullException.ThrowIfNull(document);
            var existing = await _documentRepository.GetByIdAsync(document.DocumentId);
            if (existing == null)
                return false;

            await _documentRepository.UpdateAsync(document);
            return true;
        }

        public async Task<bool> DeleteDocumentAsync(long documentId)
        {
            var existing = await _documentRepository.GetByIdAsync(documentId);
            if (existing == null)
                return false;

            await _documentRepository.DeleteAsync(documentId);
            return true;
        }
    }
}
