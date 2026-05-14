namespace StockIt.Domain.Entities;

public class Category : EntityBase
{
    public string Name { get; set; } = string.Empty;
    public Guid CompanyId { get; set; }
    public Guid UserId { get; set; }
}
