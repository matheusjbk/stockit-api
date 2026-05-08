namespace StockIt.Domain.Shared.Errors;

public class ConflictError(string message) : IError
{
    public int StatusCode => 409;

    public IEnumerable<string> Messages => [message];
}
