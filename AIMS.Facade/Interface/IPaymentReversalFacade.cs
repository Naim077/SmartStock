using AIMS.Entities;

namespace AIMS.Facade.Interface
{
    /// <summary>Read-only, detached queries for workflow-managed PaymentReversal records.</summary>
    /// <remarks>Independent mutations are not exposed; changes require the owning workflow.</remarks>
    public interface IPaymentReversalFacade
    {
        Task<List<PaymentReversal>> GetPaymentReversalsAsync(CancellationToken cancellationToken = default);
        Task<PaymentReversal?> GetPaymentReversalByIdAsync(long documentId, CancellationToken cancellationToken = default);
        Task<List<PaymentReversal>> GetPaymentReversalsPageAsync(int skip, int take, CancellationToken cancellationToken = default);
    }
}
