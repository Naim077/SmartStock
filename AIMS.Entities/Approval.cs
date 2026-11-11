
namespace AIMS.Entities;

// Maps to ss.Approval. UTC timestamps and posting rules are service responsibilities.
public sealed class Approval
{
    public long ApprovalId { get; set; }
    public long DocumentId { get; set; }
    public int VersionNo { get; set; }
    public required string Purpose { get; set; }
    public required string Decision { get; set; }
    public required string Reason { get; set; }
    public Guid ReviewedByUserId { get; set; }
    public DateTime ReviewedUtc { get; set; }

    public DocumentVersion DocumentVersion { get; set; } = null!;
    public UserProfile ReviewedByUser { get; set; } = null!;
}
