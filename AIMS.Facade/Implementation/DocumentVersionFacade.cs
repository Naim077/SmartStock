using AIMS.DataAccess.Interface;
using AIMS.Entities;
using AIMS.Facade.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIMS.Facade.Implementation
{
    public class DocumentVersionFacade : IDocumentVersionFacade
    {
        private readonly IDocumentVersionRepository _documentVersionRepository;

        public DocumentVersionFacade(IDocumentVersionRepository documentVersionRepository)
        {
            _documentVersionRepository = documentVersionRepository;
        }

        public async Task<List<DocumentVersion>> GetDocumentVersionsAsync()
        {
            return await _documentVersionRepository.GetAllAsync();
        }

        public async Task<DocumentVersion?> GetDocumentVersionByIdAsync(long documentId, int versionNo)
        {
            return await _documentVersionRepository.GetByIdAsync(documentId, versionNo);
        }

        public async Task AddDocumentVersionAsync(DocumentVersion documentVersion)
        {
            ArgumentNullException.ThrowIfNull(documentVersion);
            await _documentVersionRepository.AddAsync(documentVersion);
        }

        public async Task<bool> UpdateDocumentVersionAsync(DocumentVersion documentVersion)
        {
            ArgumentNullException.ThrowIfNull(documentVersion);
            var existing = await _documentVersionRepository.GetByIdAsync(documentVersion.DocumentId, documentVersion.VersionNo);
            if (existing == null)
                return false;

            await _documentVersionRepository.UpdateAsync(documentVersion);
            return true;
        }

        public async Task<bool> DeleteDocumentVersionAsync(long documentId, int versionNo)
        {
            var existing = await _documentVersionRepository.GetByIdAsync(documentId, versionNo);
            if (existing == null)
                return false;

            await _documentVersionRepository.DeleteAsync(documentId, versionNo);
            return true;
        }
    }
}
