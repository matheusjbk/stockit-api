namespace StockIt.Domain.Shared.Errors;

public interface IError
{
    public int StatusCode { get; }
    public IEnumerable<string> Messages { get; }
}
