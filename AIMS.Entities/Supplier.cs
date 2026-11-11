
namespace AIMS.Entities;

// Maps to ss.Supplier. UTC timestamps and posting rules are service responsibilities.
public sealed class Supplier
{
    public long SupplierId { get; set; }
    public required string Name { get; set; }
    public string? ContactName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public short PaymentTermsDays { get; set; } = (short)0;
    public decimal? ConfiguredLeadTimeDays { get; set; }
    public bool IsActive { get; set; } = true;
    public byte[] RowVersion { get; set; } = [];

    public ICollection<Product> Products { get; set; } = new List<Product>();
    public ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();
    public ICollection<OpeningAccountBalance> OpeningAccountBalances { get; set; } = new List<OpeningAccountBalance>();
    public ICollection<AccountEntry> AccountEntries { get; set; } = new List<AccountEntry>();
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    public ICollection<ReorderSuggestion> ReorderSuggestions { get; set; } = new List<ReorderSuggestion>();
}
