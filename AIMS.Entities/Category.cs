
namespace AIMS.Entities;

// Maps to ss.Category. UTC timestamps and posting rules are service responsibilities.
public sealed class Category
{
    public long CategoryId { get; set; }
    public long? ParentCategoryId { get; set; }
    public required string Name { get; set; }
    public string? NormalizedName { get; private set; }
    public byte[] RowVersion { get; set; } = [];

    public Category? ParentCategory { get; set; }
    public ICollection<Category> Categories { get; set; } = new List<Category>();
    public ICollection<Product> Products { get; set; } = new List<Product>();
}
