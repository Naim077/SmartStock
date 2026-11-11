
namespace AIMS.Entities;

// Maps to ss.Customer. UTC timestamps and posting rules are service responsibilities.
public sealed class Customer
{
    public long CustomerId { get; set; }
    public required string Name { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public decimal CreditLimit { get; set; } = 0m;
    public bool IsWalkIn { get; set; } = false;
    public bool IsActive { get; set; } = true;
    public byte[] RowVersion { get; set; } = [];

    public ICollection<Sale> Sales { get; set; } = new List<Sale>();
    public ICollection<OpeningAccountBalance> OpeningAccountBalances { get; set; } = new List<OpeningAccountBalance>();
    public ICollection<AccountEntry> AccountEntries { get; set; } = new List<AccountEntry>();
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
