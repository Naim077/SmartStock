
namespace AIMS.Entities;

// Maps to ss.AiToolCallLog. UTC timestamps and posting rules are service responsibilities.
public sealed class AiToolCallLog
{
    public long ToolCallId { get; set; }
    public long AiChatLogId { get; set; }
    public short StepNo { get; set; }
    public required string ToolName { get; set; }
    public required string ParametersRedactedJson { get; set; }
    public required string ResultDescriptorRedactedJson { get; set; }
    public long? SourcePostingId { get; set; }
    public long? ForecastRunId { get; set; }
    public long? SuggestionId { get; set; }
    public DateTime CalledUtc { get; set; }
    public DateTime? DataAsOfUtc { get; set; }
    public bool WasTruncated { get; set; }
    public required string SafeOutcomeCode { get; set; }

    public AiChatLog AiChatLog { get; set; } = null!;
    public Posting? SourcePosting { get; set; }
    public ForecastRun? ForecastRun { get; set; }
    public ReorderSuggestion? ReorderSuggestion { get; set; }
}
