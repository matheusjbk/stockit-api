using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockIt.Domain.Entities;

namespace StockIt.Infra.Data.TablesConfigurations;

public class ItemConfiguration : EntityBaseConfiguration<Item>
{
    public override void Configure(EntityTypeBuilder<Item> entity)
    {
        entity.ToTable("Items");

        base.Configure(entity);

        entity.HasOne<Category>().WithMany().HasForeignKey(i => i.CategoryId).OnDelete(DeleteBehavior.Cascade);
        entity.HasOne<Company>().WithMany().HasForeignKey(i => i.CompanyId).OnDelete(DeleteBehavior.Cascade);
        entity.HasOne<ApplicationUser>().WithMany().HasForeignKey(i => i.UserId).OnDelete(DeleteBehavior.Cascade);

        entity.Property(i => i.Name).IsRequired().HasMaxLength(100);
        entity.Property(i => i.Quantity).IsRequired();
        entity.Property(i => i.MinimumQuantity).IsRequired();
        entity.Property(i => i.Unit).HasConversion<string>().HasMaxLength(50).IsRequired();
    }
}
