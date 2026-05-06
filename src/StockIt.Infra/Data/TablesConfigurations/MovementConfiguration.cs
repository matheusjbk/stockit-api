using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockIt.Domain.Entities;

namespace StockIt.Infra.Data.TablesConfigurations;

public class MovementConfiguration : EntityBaseConfiguration<Movement>
{
    public override void Configure(EntityTypeBuilder<Movement> entity)
    {
        entity.ToTable("Movements");

        base.Configure(entity);

        entity.HasOne<Item>().WithMany().HasForeignKey(m => m.ItemId).OnDelete(DeleteBehavior.Cascade);
        entity.HasOne<ApplicationUser>().WithMany().HasForeignKey(m => m.UserId).OnDelete(DeleteBehavior.Cascade);

        entity.Property(m => m.MovementType).HasConversion<string>().HasMaxLength(50).IsRequired();
        entity.Property(m => m.Quantity).IsRequired();
        entity.Property(m => m.Date).IsRequired();
        entity.Property(m => m.Description).IsRequired().HasMaxLength(500);
    }
}
