using StockIt.Application.DTOs.Login;
using StockIt.Domain.Shared;

namespace StockIt.Application.UseCases.Auth.Login;

public interface IDoLoginUseCase
{
    public Task<Result<LoginResponse>> Execute(LoginRequest request);
}
