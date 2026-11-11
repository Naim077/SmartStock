
namespace AIMS.Entities;

// Maps to ss.DocumentVersion. UTC timestamps and posting rules are service responsibilities.
public sealed class DocumentVersion
{
    public long DocumentId { get; set; }
    public int VersionNo { get; set; }
    public required byte[] PayloadHash { get; set; }
    public required string SnapshotJson { get; set; }
    public Guid CapturedByUserId { get; set; }
    public DateTime CapturedUtc { get; set; }

    public Document Document { get; set; } = null!;
    public UserProfile CapturedByUser { get; set; } = null!;
    public ICollection<Approval> Approvals { get; set; } = new List<Approval>();
    public Posting? Posting { get; set; }
}
