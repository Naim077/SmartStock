using System.ComponentModel.DataAnnotations;

namespace AIMS.Web.Ui.Models;

public class SaleEntryViewModel
{
    public long DocumentId { get; set; }

    [Required]
    public long CustomerId { get; set; }

    public List<SaleEntryLineViewModel> Lines { get; set; } = new();
}

public class SaleEntryLineViewModel
{
    [Required]
    public long ProductId { get; set; }

    [Range(typeof(decimal), "0.01", "999999999")]
    public decimal Quantity { get; set; } = 1;

    [Range(typeof(decimal), "0", "999999999")]
    public decimal UnitPrice { get; set; }

    [Range(typeof(decimal), "0", "999999999")]
    public decimal DiscountAmount { get; set; }
}
