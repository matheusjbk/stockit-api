using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockIt.Domain.Entities;

namespace StockIt.Infra.Data.TablesConfigurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> entity)
    {
        entity.ToTable("Users");

        entity.HasKey(user => user.Id);

        entity.HasOne<Company>().WithMany().HasForeignKey(user => user.CompanyId);

        entity.Property(user => user.Id).IsRequired().ValueGeneratedNever();

        entity.Property(user => user.Name).HasMaxLength(200).IsRequired();

        entity.Property(user => user.Email).HasMaxLength(150).IsRequired().HasColumnType("varchar(150)");

        entity.Property(user => user.PasswordHash).IsRequired();

    }
}
