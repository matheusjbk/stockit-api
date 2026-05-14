using StockIt.Application.DTOs.User;
using StockIt.Application.MappingProfiles;
using StockIt.Domain.Repositories;
using StockIt.Domain.Services;
using StockIt.Domain.Shared;
using StockIt.Domain.Shared.Errors;
using StockIt.Domain.ValueObjects;

namespace StockIt.Application.UseCases.User.Register.Owner;

public class RegisterOwnerUseCase(IUserRepository userRepository, ICompanyRepository companyRepository, IAuthService authService) : IRegisterOwnerUseCase
{

    public async Task<Result<RegisteredUserResponse>> Execute(RegisterOwnerRequest request)
    {

        var validationResult = await Validate(request);

        if (!validationResult.IsSuccess) return Result<RegisteredUserResponse>.Failure(validationResult.Error!);

        // this line is mapping to user entity for apply business rules that don't yet exist
        var user = request.ToUserEntity();
        user.Role = Role.Owner.Name;

        // TODO: add migration to create companies table
        var company = request.ToCompanyEntity();
        await companyRepository.Add(company);
        user.CompanyId = company.Id;

        var createUserResult = await authService.CreateUserAsync(user, request.Password);

        if (!createUserResult.IsSuccess) return Result<RegisteredUserResponse>.Failure(createUserResult.Error!);

        var addUserToRoleResult = await authService.AddToRoleAsync(user, user.Role);

        if (!addUserToRoleResult.IsSuccess) return Result<RegisteredUserResponse>.Failure(addUserToRoleResult.Error!);

        var registeredUserResponse = user.ToRegisteredUserResponse();

        return Result<RegisteredUserResponse>.Success(registeredUserResponse);
    }

    private async Task<Result> Validate(RegisterOwnerRequest request)
    {
        var validationResult = new RegisterOwnerValidator().Validate(request);

        if(!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage);
            
            return Result.Failure(new ValidationError(errors));
        }

        var emailExistsInDb = await userRepository.ExistUserWithEmail(request.Email);

        if (emailExistsInDb) return Result.Failure(new ConflictError(ErrorMessages.EMAIL_ALREADY_REGISTERED));

        return Result.Success();

    }
}
