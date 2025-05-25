namespace NoteTakingApp.Domain.Common;

public record Result
{
    public bool IsSuccess { get; }
    public string? Error { get; } = null;
    
    protected Result(bool isSuccess)
    {
        IsSuccess = isSuccess;
    }

    protected Result(bool isSuccess, string? error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success() => new(true);

    public static Result Failure(string? error) => new(false, error);

    public static Result<T> Success<T>(T value) => new(value, true);
    
    public static Result<T?> Failure<T>(string? error) => new(default, false, error);
}

public record Result<T> : Result
{
    public T Value { get; }
    
    protected internal Result(T value, bool isSuccess)
        : base(isSuccess)
    {
        Value = value;
    }

    protected internal Result(T value, bool isSuccess, string? error)
        : base(isSuccess, error)
    {
        Value = value;
    }
}
