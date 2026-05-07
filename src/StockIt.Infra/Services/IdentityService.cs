using Microsoft.AspNetCore.Identity;
using StockIt.Domain.Services;
using StockIt.Infra.Data.TablesConfigurations;

namespace StockIt.Infra.Services;

public class IdentityService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public IdentityService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<bool> CreateUserAsync(string name, string email, string password)
    {
        var result = await _userManager.CreateAsync(new ApplicationUser
        {
            UserName = name,
            Email = email,
        }, password);

        return result.Succeeded;
    }

}
