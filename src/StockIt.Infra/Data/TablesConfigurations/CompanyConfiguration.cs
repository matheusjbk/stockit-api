using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockIt.Domain.Entities;

namespace StockIt.Infra.Data.TablesConfigurations;

public class CompanyConfiguration : EntityBaseConfiguration<Company>
{
    public override void Configure(EntityTypeBuilder<Company> entity)
    {
        entity.ToTable("Companies");

        base.Configure(entity);

        entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
    }
}
