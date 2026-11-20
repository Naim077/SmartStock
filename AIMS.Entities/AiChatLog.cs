using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AIMS.Entities;

[Table("AiChatLog", Schema = "ss")]
public class AiChatLog
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long AiChatLogId { get; set; }
    public Guid UserId { get; set; }
    public Guid CorrelationId { get; set; }
    public DateTime StartedUtc { get; set; }
    public DateTime? CompletedUtc { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string Status { get; set; } = null!;
    [Column(TypeName = "nvarchar(max)")]
    public string? Provider { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string? Model { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string QuestionRedacted { get; set; } = null!;
    [Column(TypeName = "nvarchar(max)")]
    public string? AnswerRedacted { get; set; }
    public DateTime BudgetMonth { get; set; }
    public int ReservedTokens { get; set; }
    public int InputTokens { get; set; }
    public int OutputTokens { get; set; }
    public int? LatencyMilliseconds { get; set; }
    [Column(TypeName = "nvarchar(max)")]
    public string? SafeFailureCode { get; set; }
    public DateTime RetainUntilUtc { get; set; }
    public bool LegalHold { get; set; }
    [Timestamp]
    public byte[] RowVersion { get; set; } = null!;
}
