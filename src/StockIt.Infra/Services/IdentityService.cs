using Microsoft.AspNetCore.Identity;
using StockIt.Domain.Entities;
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

    public async Task<bool> CreateUserAsync(User user, string password)
    {
        var result = await _userManager.CreateAsync(new ApplicationUser
        {
            Id = user.Id,
            UserName = user.Name,
            Email = user.Email,
        }, password);

        return result.Succeeded;
    }

}
