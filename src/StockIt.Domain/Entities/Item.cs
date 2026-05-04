using StockIt.Domain.Enums;

namespace StockIt.Domain.Entities;

public class Item : EntityBase
{
    public string Name { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public decimal MinimumQuantity { get; set; }
    public ItemMeasureUnit Unit { get; set; }
    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = default!;
    public Guid UserId { get; set; }
    public User User { get; set; } = default!;
}
