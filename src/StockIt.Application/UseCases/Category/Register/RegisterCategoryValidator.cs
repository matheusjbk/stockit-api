using FluentValidation;
using StockIt.Application.DTOs.Category;
using StockIt.Domain.Shared;

namespace StockIt.Application.UseCases.Category.Register;

public class RegisterCategoryValidator : AbstractValidator<CategoryRequest>
{
    public RegisterCategoryValidator()
    {
        RuleFor(request => request.Name).NotEmpty().WithMessage(ErrorMessages.EMPTY_CATEGORY_NAME);

        When(request => !string.IsNullOrWhiteSpace(request.Name), () => RuleFor(request => request.Name).MinimumLength(2).WithMessage(ErrorMessages.SHORT_CATEGORY_NAME));
    }
}
