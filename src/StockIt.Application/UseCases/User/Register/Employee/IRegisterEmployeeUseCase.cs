using StockIt.Application.DTOs.User;
using StockIt.Domain.Shared;

namespace StockIt.Application.UseCases.User.Register.Employee;

public interface IRegisterEmployeeUseCase
{
    public Task<Result<RegisteredUserResponse>> Execute(RegisterUserRequest request);
}
