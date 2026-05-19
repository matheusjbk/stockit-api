using StockIt.Application.DTOs.User;
using StockIt.Domain.Shared;

namespace StockIt.Application.UseCases.User.Profile;

public interface IGetUserProfileUseCase
{
    public Task<Result<UserProfileResponse>> Execute();
}
