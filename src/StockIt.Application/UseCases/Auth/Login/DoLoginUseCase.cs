using StockIt.Application.DTOs.Login;
using StockIt.Application.Security.Tokens;
using StockIt.Application.Services;
using StockIt.Domain.Shared;

namespace StockIt.Application.UseCases.Auth.Login;

public class DoLoginUseCase(IAuthService authService, IAccessTokenGenerator accessTokenGenerator) : IDoLoginUseCase
{

    public async Task<Result<LoginResponse>> Execute(LoginRequest request)
    {
        var result = await authService.LoginAsync(request.Email, request.Password);

        if(!result.IsSuccess) return Result<LoginResponse>.Failure(result.Error!);

        var token = accessTokenGenerator.GenerateToken(result.Value!);

        var loginResponse = new LoginResponse(token);

        return Result<LoginResponse>.Success(loginResponse);
    }
}
