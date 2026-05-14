namespace StockIt.Domain.ValueObjects;

public class Role
{
    public static readonly Role Owner = new("Owner");
    public static readonly Role Employee = new("Employee");

    public string Name { get; set; } = string.Empty;

    private Role(string name) => Name = name;

    public static IEnumerable<Role> GetAll()
    {
        yield return Owner;
        yield return Employee;
    }
}
