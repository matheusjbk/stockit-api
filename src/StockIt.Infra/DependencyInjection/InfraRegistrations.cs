using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StockIt.Application.Security.Tokens;
using StockIt.Application.Services;
using StockIt.Domain.Repositories;
using StockIt.Infra.Data;
using StockIt.Infra.Data.Repositories;
using StockIt.Infra.Data.TablesConfigurations;
using StockIt.Infra.Security.Tokens;
using StockIt.Infra.Services;

namespace StockIt.Infra.DependencyInjection;

public static class InfraRegistrations
{
    public static void AddInfra(this IServiceCollection services, IConfiguration configuration)
    {
        AddDbContext(services, configuration);
        AddIdentity(services);
        AddAuthService(services);
        AddRepositories(services);
        AddTokens(services, configuration);
        AddLoggedUser(services);
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Postgres");

        services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));
    }

    private static void AddIdentity(IServiceCollection services)
    {
        services.AddIdentityCore<ApplicationUser>().AddRoles<IdentityRole<Guid>>().AddEntityFrameworkStores<AppDbContext>();
    }

    private static void AddAuthService(IServiceCollection services)
    {
        services.AddScoped<IAuthService, IdentityService>();
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    private static void AddTokens(IServiceCollection services, IConfiguration configuration)
    {
        var expirationTime = configuration.GetValue<uint>("Settings:Jwt:ExpirationTimeInMinutes");
        var secretKey = configuration.GetValue<string>("Settings:Jwt:SecretKey")!;

        services.AddScoped<IAccessTokenGenerator>(provider => new JwtGenerator(expirationTime, secretKey));
    }

    private static void AddLoggedUser(IServiceCollection services) => services.AddScoped<ILoggedUser, LoggedUser>();
}
