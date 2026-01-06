using EcommerceLearn.Domain.Common.Results;

public class Result
{
    public bool IsSuccess { get; }
    public Errors? Error { get; }

    protected Result(bool success, Errors? error)
    { IsSuccess = success; Error = error; }

    public static Result Success() => new(true, null);
    public static Result Failure(Errors error) => new(false, error);
}

public sealed class Result<T> : Result
{
    public T? Value { get; }
    private Result(bool success, T? value, Errors? error) : base(success, error)
    { Value = value; }

    public static Result<T> Success(T value) => new(true, value, null);
    public static new Result<T> Failure(Errors error) => new(false, default, error);
}