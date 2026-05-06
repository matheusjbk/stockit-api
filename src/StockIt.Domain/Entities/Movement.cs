using StockIt.Domain.Enums;

namespace StockIt.Domain.Entities;

public class Movement : EntityBase
{
    public MovementType MovementType { get; set; }
    public decimal Quantity { get; set; }
    public DateTimeOffset Date { get; set; } = DateTimeOffset.UtcNow;
    public string Description { get; set; } = string.Empty;
    public Guid ItemId { get; set; }
    public Guid UserId { get; set; }
}
