using StockIt.Application.DTOs.User;

namespace StockIt.Application.UseCases.User.Register;

public interface IRegisterUserUseCase
{
    public Task<RegisteredUserResponse> Execute(RegisterUserRequest request);
}
