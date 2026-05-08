using FluentValidation;
using FluentValidation.Validators;
using StockIt.Domain.Shared;

namespace StockIt.Application.Validators;

public class PasswordValidator<T> : PropertyValidator<T, string>
{
    public override string Name => "PasswordValidator";

    public override bool IsValid(ValidationContext<T> context, string password)
    {
        if(string.IsNullOrWhiteSpace(password))
        {
            context.MessageFormatter.AppendArgument("ErrorMessage", ErrorMessages.EMPTY_PASSWORD);
            return false;
        }

        if(password.Length <= 5)
        {
            context.MessageFormatter.AppendArgument("ErrorMessage", ErrorMessages.SHORT_PASSWORD);
            return false;
        }

        return true;
    }

    protected override string GetDefaultMessageTemplate(string errorCode) => "{ErrorMessage}";
}
