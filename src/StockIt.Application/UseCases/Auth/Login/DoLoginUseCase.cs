using StockIt.Application.DTOs.Login;
using StockIt.Domain.Repositories;
using StockIt.Domain.Security.Tokens;
using StockIt.Domain.Services;
using StockIt.Domain.Shared;
using StockIt.Domain.Shared.Errors;

namespace StockIt.Application.UseCases.Auth.Login;

public class DoLoginUseCase : IDoLoginUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthService _authService;
    private readonly IAccessTokenGenerator _accessTokenGenerator;

    public DoLoginUseCase(IUserRepository userRepository, IAuthService authService, IAccessTokenGenerator accessTokenGenerator)
    {
        _userRepository = userRepository;
        _authService = authService;
        _accessTokenGenerator = accessTokenGenerator;
    }

    public async Task<Result<LoginResponse>> Execute(LoginRequest request)
    {
        var user = await _userRepository.GetUserByEmail(request.Email);

        if (user is null) return Result<LoginResponse>.Failure(new UnauthorizedError(ErrorMessages.INVALID_LOGIN));

        var result = await _authService.LoginAsync(request.Email, request.Password);

        if(!result.IsSuccess) return Result<LoginResponse>.Failure(result.Error!);

        var token = _accessTokenGenerator.GenerateToken(user);

        var loginResponse = new LoginResponse(token);

        return Result<LoginResponse>.Success(loginResponse);
    }
}
