using FluentValidation;
using FluentValidation.Validators;

namespace StockIt.Application.Validators;

public class PasswordValidator<T> : PropertyValidator<T, string>
{
    public override string Name => "PasswordValidator";

    public override bool IsValid(ValidationContext<T> context, string password)
    {
        if(string.IsNullOrWhiteSpace(password))
        {
            context.MessageFormatter.AppendArgument("ErrorMessage", "");
            return false;
        }

        if(password.Length <= 5)
        {
            context.MessageFormatter.AppendArgument("ErrorMessage", "");
            return false;
        }

        return true;
    }

    protected override string GetDefaultMessageTemplate(string errorCode) => "{ErrorMessage}";
}
