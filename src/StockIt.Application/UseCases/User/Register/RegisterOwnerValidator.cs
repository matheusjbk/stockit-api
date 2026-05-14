using FluentValidation;
using StockIt.Application.DTOs.User;
using StockIt.Application.Validators;
using StockIt.Domain.Shared;

namespace StockIt.Application.UseCases.User.Register;

public class RegisterOwnerValidator : AbstractValidator<RegisterOwnerRequest>
{
    public RegisterOwnerValidator()
    {
        RuleFor(request => request.Name).NotEmpty().WithMessage(ErrorMessages.EMPTY_NAME);
        RuleFor(request => request.Email).NotEmpty().WithMessage(ErrorMessages.EMPTY_EMAIL);
        RuleFor(request => request.CompanyName).NotEmpty().WithMessage(ErrorMessages.EMPTY_COMPANY_NAME);
        RuleFor(request => request.Password).SetValidator(new PasswordValidator<RegisterOwnerRequest>());

        When(request => !string.IsNullOrWhiteSpace(request.Name), () => RuleFor(request => request.Name).MinimumLength(2).WithMessage(ErrorMessages.SHORT_NAME));
        When(request => !string.IsNullOrWhiteSpace(request.Email), () => RuleFor(request => request.Email).EmailAddress().WithMessage(ErrorMessages.INVALID_EMAIL));
        When(request => !string.IsNullOrWhiteSpace(request.CompanyName), () => RuleFor(request => request.CompanyName).MinimumLength(2).WithMessage(ErrorMessages.SHORT_COMPANY_NAME));
    }
}
