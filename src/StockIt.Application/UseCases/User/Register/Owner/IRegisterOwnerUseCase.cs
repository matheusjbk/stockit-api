using StockIt.Application.DTOs.User;
using StockIt.Domain.Shared;

namespace StockIt.Application.UseCases.User.Register.Owner;

public interface IRegisterOwnerUseCase
{
    public Task<Result<RegisteredUserResponse>> Execute(RegisterOwnerRequest request);
}
