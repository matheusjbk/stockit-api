using Microsoft.AspNetCore.Identity;
using StockIt.Domain.Entities;
using StockIt.Domain.Services;
using StockIt.Domain.Shared;
using StockIt.Domain.Shared.Errors;
using StockIt.Infra.Data.TablesConfigurations;

namespace StockIt.Infra.Services;

public class IdentityService(UserManager<ApplicationUser> userManager) : IAuthService
{

    public async Task<Result> CreateUserAsync(User user, string password)
    {
        var result = await userManager.CreateAsync(new ApplicationUser
        {
            Id = user.Id,
            Name = user.Name,
            UserName = user.Email,
            Email = user.Email,
            CompanyId = user.CompanyId,
        }, password);

        if (result.Succeeded) return Result.Success();

        var errors = MapIdentityError(result.Errors);

        return Result.Failure(new AggregateError(errors));
    }

    public async Task<Result> LoginAsync(string email, string password)
    {
        var user = await userManager.FindByEmailAsync(email);

        if (user is null || await userManager.CheckPasswordAsync(user, password))
            return Result.Failure(new UnauthorizedError(ErrorMessages.INVALID_LOGIN));

        return Result.Success();
    }

    private static IEnumerable<IError> MapIdentityError(IEnumerable<IdentityError> errors)
    {
        foreach(var error in errors)
        {
            yield return error.Code switch
            {
                "DuplicateEmail" => new ConflictError(ErrorMessages.EMAIL_ALREADY_REGISTERED),
                "InvalidEmail" => new ValidationError([ErrorMessages.INVALID_EMAIL]),
                "PasswordTooShort" => new ValidationError([ErrorMessages.SHORT_PASSWORD]),
                "PasswordRequiresNonAlphanumeric" => new ValidationError([ErrorMessages.NON_ALPHA_PASSWORD]),
                "PasswordRequiresDigit" => new ValidationError([ErrorMessages.NON_DIGIT_PASSWORD]),
                "PasswordRequiresLower" => new ValidationError([ErrorMessages.NON_LOWER_PASSWORD]),
                "PasswordRequiresUpper" => new ValidationError([ErrorMessages.NON_UPPER_PASSWORD]),
                _ => new ValidationError([error.Description]),
            };
        }
    }

}
