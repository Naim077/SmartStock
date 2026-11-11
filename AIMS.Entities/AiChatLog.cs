
namespace AIMS.Entities;

// Maps to ss.AiChatLog. UTC timestamps and posting rules are service responsibilities.
public sealed class AiChatLog
{
    public long AiChatLogId { get; set; }
    public Guid UserId { get; set; }
    public Guid CorrelationId { get; set; }
    public DateTime StartedUtc { get; set; }
    public DateTime? CompletedUtc { get; set; }
    public required string Status { get; set; }
    public string? Provider { get; set; }
    public string? Model { get; set; }
    public required string QuestionRedacted { get; set; }
    public string? AnswerRedacted { get; set; }
    public DateOnly BudgetMonth { get; set; }
    public int ReservedTokens { get; set; }
    public int InputTokens { get; set; }
    public int OutputTokens { get; set; }
    public int? LatencyMilliseconds { get; set; }
    public string? SafeFailureCode { get; set; }
    public DateTime RetainUntilUtc { get; set; }
    public bool LegalHold { get; set; } = false;
    public byte[] RowVersion { get; set; } = [];

    public UserProfile UserProfile { get; set; } = null!;
    public ICollection<AiToolCallLog> AiToolCallLogs { get; set; } = new List<AiToolCallLog>();
}
