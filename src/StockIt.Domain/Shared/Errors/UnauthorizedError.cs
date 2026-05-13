namespace StockIt.Domain.Shared.Errors;

public class UnauthorizedError(string message) : IError
{
    public int StatusCode => 401;
    public IEnumerable<string> Messages => [message];
}
