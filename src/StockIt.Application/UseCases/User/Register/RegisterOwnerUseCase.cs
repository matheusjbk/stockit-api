using StockIt.Application.DTOs.User;
using StockIt.Application.MappingProfiles;
using StockIt.Domain.Repositories;
using StockIt.Domain.Services;
using StockIt.Domain.Shared;
using StockIt.Domain.Shared.Errors;

namespace StockIt.Application.UseCases.User.Register;

public class RegisterOwnerUseCase(IUserRepository userRepository, IAuthService authService) : IRegisterOwnerUseCase
{

    public async Task<Result<RegisteredUserResponse>> Execute(RegisterOwnerRequest request)
    {

        var validationResult = await Validate(request);

        if (!validationResult.IsSuccess) return Result<RegisteredUserResponse>.Failure(validationResult.Error!);

        // this line is mapping to user entity for apply business rules that don't yet exist
        var user = request.ToUserEntity();

        // TODO: add migration to create companies table
        // TODO: create company and save it in database
        //request.CompanyName;
        // TODO: add company id in user entity
        //user.CompanyId = company.Id;

        var result = await authService.CreateUserAsync(user, request.Password);

        if (!result.IsSuccess) return Result<RegisteredUserResponse>.Failure(result.Error!);

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
