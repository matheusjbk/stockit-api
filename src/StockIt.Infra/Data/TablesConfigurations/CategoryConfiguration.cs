using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockIt.Domain.Entities;

namespace StockIt.Infra.Data.TablesConfigurations;

public class CategoryConfiguration : EntityBaseConfiguration<Category>
{
    public override void Configure(EntityTypeBuilder<Category> entity)
    {
        entity.ToTable("Categories");
            
        base.Configure(entity);

        entity.HasOne<Company>().WithMany().HasForeignKey(c => c.CompanyId).OnDelete(DeleteBehavior.Cascade);

        entity.HasOne<ApplicationUser>().WithMany().HasForeignKey(c => c.UserId).OnDelete(DeleteBehavior.Cascade);
    
        entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
    }
}
