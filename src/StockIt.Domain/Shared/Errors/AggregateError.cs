namespace StockIt.Domain.Shared.Errors;

public class AggregateError : IError
{
    public int StatusCode => 400;
    public IEnumerable<IError> SubErrors { get; }

    public IEnumerable<string> Messages => SubErrors.SelectMany(e => e.Messages);

    public AggregateError(IEnumerable<IError> errors)
    {
        SubErrors = errors;
    }
}
