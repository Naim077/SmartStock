namespace AIMS.Entities;

public static class ApplicationRoles
{
    public const string Owner = "Owner";
    public const string Manager = "Manager";
    public const string SalesStaff = "Sales Staff";
    public const string Storekeeper = "Storekeeper";

    public static IReadOnlyList<string> All { get; } =
        Array.AsReadOnly(new[] { Owner, Manager, SalesStaff, Storekeeper });
}
