using Microsoft.Extensions.DependencyInjection;
using StockIt.Application.UseCases.Auth.Login;
using StockIt.Application.UseCases.User.Register;

namespace StockIt.Application.DependencyInjection;

public static class ApplicationRegistrations
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddUseCases(services);
    }

    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<IRegisterOwnerUseCase, RegisterOwnerUseCase>();

        services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();
    }
}
