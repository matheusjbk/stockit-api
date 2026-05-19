using StockIt.Application.DTOs.User;
using StockIt.Application.Services;
using StockIt.Domain.Repositories;
using StockIt.Domain.Shared;

namespace StockIt.Application.UseCases.User.Profile;

public class GetUserProfileUseCase(IUnitOfWork unitOfWork, ILoggedUser loggedUser) : IGetUserProfileUseCase
{
    public async Task<Result<UserProfileResponse>> Execute()
    {
        var user = await unitOfWork.Users.GetUserByEmail(loggedUser.GetUserEmail());

        var company = await unitOfWork.Companies.GetById(loggedUser.GetCompanyId());

        var profile = new UserProfileResponse(user!.Name, user.Email, company!.Name, loggedUser.GetUserRole());

        return Result<UserProfileResponse>.Success(profile);
    }
}
