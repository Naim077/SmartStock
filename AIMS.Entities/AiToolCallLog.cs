using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AIMS.Entities;

[Table("AiToolCallLog", Schema = "ss")]
public class AiToolCallLog
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long ToolCallId { get; set; }
    public long AiChatLogId { get; set; }
    public short StepNo { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string ToolName { get; set; } = null!;
    [Column(TypeName = "nvarchar(max)")]
    public string ParametersRedactedJson { get; set; } = null!;
    [Column(TypeName = "nvarchar(max)")]
    public string ResultDescriptorRedactedJson { get; set; } = null!;
    public long? SourcePostingId { get; set; }
    public long? ForecastRunId { get; set; }
    public long? SuggestionId { get; set; }
    public DateTime CalledUtc { get; set; }
    public DateTime? DataAsOfUtc { get; set; }
    public bool WasTruncated { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string SafeOutcomeCode { get; set; } = null!;
}
