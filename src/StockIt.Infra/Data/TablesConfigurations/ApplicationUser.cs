using Microsoft.AspNetCore.Identity;

namespace StockIt.Infra.Data.TablesConfigurations;

public class ApplicationUser : IdentityUser<Guid>
{
    public ApplicationUser()
    {
        Id = Guid.CreateVersion7();
    }

    public string Name { get; set; } = string.Empty;
}
