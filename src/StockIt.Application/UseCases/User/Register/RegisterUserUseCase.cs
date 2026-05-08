using StockIt.Application.DTOs.User;
using StockIt.Application.MappingProfiles;
using StockIt.Domain.Repositories;
using StockIt.Domain.Services;
using StockIt.Domain.Shared;
using StockIt.Domain.Shared.Errors;

namespace StockIt.Application.UseCases.User.Register;

public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthService _authService;

    public RegisterUserUseCase(IUserRepository userRepository, IAuthService authService)
    {
        _userRepository = userRepository;
        _authService = authService;
    }

    public async Task<Result<RegisteredUserResponse>> Execute(RegisterUserRequest request)
    {

        var validationResult = await Validate(request);

        if (!validationResult.IsSuccess) return Result<RegisteredUserResponse>.Failure(validationResult.Error!);

        // this line is mapping to user entity for apply business rules that don't yet exist
        var user = request.ToUserEntity();

        var result = await _authService.CreateUserAsync(user, request.Password);

        //TODO: refactor to return identity error messages
        if (!result) return Result<RegisteredUserResponse>.Failure(new InternalServerError());

        var registeredUserResponse = user.ToRegisteredUserResponse();

        return Result<RegisteredUserResponse>.Success(registeredUserResponse);
    }

    private async Task<Result> Validate(RegisterUserRequest request)
    {
        var validationResult = new RegisterUserValidator().Validate(request);

        if(!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage);
            
            return Result.Failure(new ValidationError(errors));
        }

        var emailExistsInDb = await _userRepository.ExistUserWithEmail(request.Email);

        if (emailExistsInDb) return Result.Failure(new ConflictError(ErrorMessages.EMAIL_ALREADY_REGISTERED));

        return Result.Success();

    }
}
