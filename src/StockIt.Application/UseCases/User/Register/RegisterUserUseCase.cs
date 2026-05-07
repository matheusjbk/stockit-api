using StockIt.Application.DTOs.User;
using StockIt.Application.MappingProfiles;
using StockIt.Domain.Repositories;
using StockIt.Domain.Services;

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

    public async Task<RegisteredUserResponse> Execute(RegisterUserRequest request)
    {
        //TODO: create result pattern w/ errors
        //TODO: create error messages

        var validationResult = await Validate(request);

        if (!validationResult) return null;

        var result = await _authService.CreateUserAsync(request.Name, request.Email, request.Password);

        if (!result) return null;

        return request.ToRegisteredUserResponse();
    }

    private async Task<bool> Validate(RegisterUserRequest request)
    {
        var validationResult = new RegisterUserValidator().Validate(request);

        if(!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage);
        }

        var emailExistsInDb = await _userRepository.ExistUserWithEmail(request.Email);

        if (emailExistsInDb) return false;

        return true;

    }
}
