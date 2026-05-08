namespace StockIt.Domain.Shared.Errors;

public class InternalServerError : IError
{
    public int StatusCode => 500;

    public IEnumerable<string> Messages => [ErrorMessages.UNKNOWN_ERROR];
}
