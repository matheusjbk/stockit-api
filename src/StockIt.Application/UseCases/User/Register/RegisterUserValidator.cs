using FluentValidation;
using StockIt.Application.DTOs.User;
using StockIt.Application.Validators;

namespace StockIt.Application.UseCases.User.Register;

public class RegisterUserValidator : AbstractValidator<RegisterUserRequest>
{
    public RegisterUserValidator()
    {
        RuleFor(request => request.Name).NotEmpty();
        RuleFor(request => request.Email).NotEmpty();
        RuleFor(request => request.Password).SetValidator(new PasswordValidator<RegisterUserRequest>());

        When(request => !string.IsNullOrWhiteSpace(request.Name), () => RuleFor(request => request.Name).MinimumLength(2));
        When(request => !string.IsNullOrWhiteSpace(request.Email), () => RuleFor(request => request.Email).EmailAddress());
    }
}
