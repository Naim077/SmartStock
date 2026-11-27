using System.ComponentModel.DataAnnotations;

namespace AIMS.Web.Ui.Models;

public class StockAdjustmentViewModel
{
    public long DocumentId { get; set; }

    [Required, StringLength(100)]
    public string ChangeKind { get; set; } = "CountAdjustment";

    [Required, StringLength(1000)]
    public string Reason { get; set; } = string.Empty;

    [StringLength(1000)]
    public string? EvidenceReference { get; set; }

    public List<StockAdjustmentLineViewModel> Lines { get; set; } = new();
}

public class StockAdjustmentLineViewModel
{
    [Range(1, long.MaxValue)]
    public long ProductId { get; set; }

    [Range(1, long.MaxValue)]
    public long ProductBatchId { get; set; }

    [Required, StringLength(100)]
    public string Bucket { get; set; } = string.Empty;

    [Range(typeof(decimal), "-999999999", "999999999")]
    public decimal QuantityDelta { get; set; }
}
