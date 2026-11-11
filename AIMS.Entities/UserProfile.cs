
namespace AIMS.Entities;

// Maps to ss.UserProfile. UTC timestamps and posting rules are service responsibilities.
public sealed class UserProfile
{
    public Guid UserId { get; set; }
    public required string DisplayName { get; set; }
    public bool IsActive { get; set; } = true;
    public bool MustChangePassword { get; set; } = false;
    public DateTime CreatedUtc { get; set; }
    public DateTime? DeactivatedUtc { get; set; }
    public byte[] RowVersion { get; set; } = [];

    public ApplicationUser IdentityUser { get; set; } = null!;
    public ICollection<BusinessSettings> BusinessSettings { get; set; } = new List<BusinessSettings>();
    public ICollection<IdempotencyRequest> IdempotencyRequests { get; set; } = new List<IdempotencyRequest>();
    public ICollection<ImportBatch> ImportBatches { get; set; } = new List<ImportBatch>();
    public ICollection<ImportBatch> ImportBatchesAsConfirmedByUser { get; set; } = new List<ImportBatch>();
    public ICollection<Document> Documents { get; set; } = new List<Document>();
    public ICollection<DocumentVersion> DocumentVersions { get; set; } = new List<DocumentVersion>();
    public ICollection<Approval> Approvals { get; set; } = new List<Approval>();
    public ICollection<Posting> Postings { get; set; } = new List<Posting>();
    public ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();
    public ICollection<AccountAllocation> AccountAllocations { get; set; } = new List<AccountAllocation>();
    public ICollection<PaymentSettlementEvent> PaymentSettlementEvents { get; set; } = new List<PaymentSettlementEvent>();
    public ICollection<JobRun> JobRuns { get; set; } = new List<JobRun>();
    public ICollection<ReorderSuggestion> ReorderSuggestions { get; set; } = new List<ReorderSuggestion>();
    public ICollection<SuggestionConversion> SuggestionConversions { get; set; } = new List<SuggestionConversion>();
    public ICollection<SuggestionConversion> SuggestionConversionsAsReleasedByUser { get; set; } = new List<SuggestionConversion>();
    public ICollection<AiChatLog> AiChatLogs { get; set; } = new List<AiChatLog>();
    public ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();
    public ICollection<AuditLog> AuditLogsAsTargetUser { get; set; } = new List<AuditLog>();
    public ICollection<Alert> Alerts { get; set; } = new List<Alert>();
}
