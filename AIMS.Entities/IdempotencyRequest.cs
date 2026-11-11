
namespace AIMS.Entities;

// Maps to ss.IdempotencyRequest. UTC timestamps and posting rules are service responsibilities.
public sealed class IdempotencyRequest
{
    public long RequestId { get; set; }
    public Guid ActorUserId { get; set; }
    public required string Operation { get; set; }
    public Guid RequestKey { get; set; }
    public required byte[] PayloadHash { get; set; }
    public required string RequestStatus { get; set; }
    public DateTime CreatedUtc { get; set; }
    public DateTime? CompletedUtc { get; set; }
    public string? ResultJson { get; set; }

    public UserProfile ActorUser { get; set; } = null!;
    public ICollection<ImportBatch> ImportBatches { get; set; } = new List<ImportBatch>();
    public ICollection<Document> Documents { get; set; } = new List<Document>();
    public ICollection<Posting> Postings { get; set; } = new List<Posting>();
    public ICollection<AccountAllocation> AccountAllocations { get; set; } = new List<AccountAllocation>();
    public ICollection<JobRun> JobRuns { get; set; } = new List<JobRun>();
    public ICollection<SuggestionConversion> SuggestionConversions { get; set; } = new List<SuggestionConversion>();
}
