using AIMS.Entities;

namespace AIMS.Facade.Interface
{
    /// <summary>Read-only, detached queries for workflow-managed Payment records.</summary>
    /// <remarks>Independent mutations are not exposed; changes require the owning workflow.</remarks>
    public interface IPaymentFacade
    {
        Task<List<Payment>> GetPaymentsAsync(CancellationToken cancellationToken = default);
        Task<Payment?> GetPaymentByIdAsync(long documentId, CancellationToken cancellationToken = default);
        Task<List<Payment>> GetPaymentsPageAsync(int skip, int take, CancellationToken cancellationToken = default);
    }
}
