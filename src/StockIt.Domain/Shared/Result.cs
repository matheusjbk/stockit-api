using StockIt.Domain.Shared.Errors;

namespace StockIt.Domain.Shared;

public class Result
{
    public bool IsSuccess { get; private set; }
    public IError? Error { get; private set; }

    protected Result(bool isSuccess = true, IError? error = null)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success() => new();
    public static Result Failure(IError error) => new(false, error);
}

public class Result<T> : Result
{
    private readonly T? _value;
    public T? Value => IsSuccess ? _value : default;

    protected Result(T? value = default, bool isSuccess = true, IError? error = null) : base(isSuccess, error) => _value = value;

    public static Result<T> Success(T value) => new(value);

    public static new Result<T> Failure(IError error) => new(isSuccess: false, error: error);
}
