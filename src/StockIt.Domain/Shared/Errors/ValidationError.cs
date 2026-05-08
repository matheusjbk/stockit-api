namespace StockIt.Domain.Shared.Errors;

public class ValidationError(IEnumerable<string> messages) : IError
{
    public int StatusCode => 400;
    public IEnumerable<string> Messages => messages;
}
