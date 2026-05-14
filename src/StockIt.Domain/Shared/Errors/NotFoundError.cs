namespace StockIt.Domain.Shared.Errors;

public class NotFoundError(string message) : IError
{
    public int StatusCode => 404;

    public IEnumerable<string> Messages => [message];
}
