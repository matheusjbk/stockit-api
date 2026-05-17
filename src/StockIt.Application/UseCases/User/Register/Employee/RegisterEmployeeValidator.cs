using FluentValidation;
using StockIt.Application.DTOs.User;
using StockIt.Application.Validators;
using StockIt.Domain.Shared;

namespace StockIt.Application.UseCases.User.Register.Employee;

public class RegisterEmployeeValidator : AbstractValidator<RegisterUserRequest>
{
    public RegisterEmployeeValidator()
    {
        RuleFor(request => request.Name).NotEmpty().WithMessage(ErrorMessages.EMPTY_NAME);
        RuleFor(request => request.Email).NotEmpty().WithMessage(ErrorMessages.EMPTY_EMAIL);
        RuleFor(request => request.CompanyName).Empty().WithMessage(ErrorMessages.COMPANY_NAME_NOT_ALLOWED);
        RuleFor(request => request.Password).SetValidator(new PasswordValidator<RegisterUserRequest>());

        When(request => !string.IsNullOrWhiteSpace(request.Name), () => RuleFor(request => request.Name).MinimumLength(2).WithMessage(ErrorMessages.SHORT_NAME));
        When(request => !string.IsNullOrWhiteSpace(request.Email), () => RuleFor(request => request.Email).EmailAddress().WithMessage(ErrorMessages.INVALID_EMAIL));
    }
}
