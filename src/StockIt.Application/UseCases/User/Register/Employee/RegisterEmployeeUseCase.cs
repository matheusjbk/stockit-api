using StockIt.Application.DTOs.User;
using StockIt.Application.MappingProfiles;
using StockIt.Application.Services;
using StockIt.Domain.Repositories;
using StockIt.Domain.Shared;
using StockIt.Domain.Shared.Errors;
using StockIt.Domain.ValueObjects;

namespace StockIt.Application.UseCases.User.Register.Employee;

public class RegisterEmployeeUseCase(IUnitOfWork unitOfWork, IAuthService authService, ILoggedUser loggedUser) : IRegisterEmployeeUseCase
{
    public async Task<Result<RegisteredUserResponse>> Execute(RegisterUserRequest request)
    {
        var validationResult = await Validate(request);

        if (!validationResult.IsSuccess) return Result<RegisteredUserResponse>.Failure(validationResult.Error!);

        // this line is mapping to user entity for apply business rules that don't yet exist
        var user = request.ToUserEntity();
        user.Role = Role.Employee.Name;
        user.CompanyId = loggedUser.GetCompanyId();

        var createUserResult = await authService.CreateUserAsync(user, request.Password);

        if (!createUserResult.IsSuccess) return Result<RegisteredUserResponse>.Failure(createUserResult.Error!);

        var addUserToRoleResult = await authService.AddToRoleAsync(user, user.Role);

        if (!addUserToRoleResult.IsSuccess) return Result<RegisteredUserResponse>.Failure(addUserToRoleResult.Error!);

        var registeredUserResponse = user.ToRegisteredUserResponse();

        return Result<RegisteredUserResponse>.Success(registeredUserResponse);
    }

    private async Task<Result> Validate(RegisterUserRequest request)
    {
        var validationResult = new RegisterEmployeeValidator().Validate(request);

        if(!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage);

            return Result.Failure(new ValidationError(errors));
        }

        var emailExistsInDb = await unitOfWork.Users.ExistUserWithEmail(request.Email);

        if (emailExistsInDb) return Result.Failure(new ConflictError(ErrorMessages.EMAIL_ALREADY_REGISTERED));

        return Result.Success();
    }
}
