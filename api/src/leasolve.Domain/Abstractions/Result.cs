using System.Diagnostics.CodeAnalysis;

namespace leasolve.Domain.Abstractions;

// NOTE: maybe in future result related files from domain to external library
public class Result
{
    public bool IsSuccess { get; }
    
    [MemberNotNullWhen(true, nameof(Error))]
    public bool IsFailure => !IsSuccess;
    public Error? Error { get; }
    
    
    protected Result(bool isSuccess, Error? error)
    {
        if (isSuccess && error is not null)
            throw new ArgumentException("Success cannot carry an error.", nameof(error));

        if (!isSuccess && error is null)
            throw new ArgumentException("Failure must carry an error.", nameof(error));

        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success() => new(true, null);
    public static Result<T> Success<T>(T value) => new(value, true, null);

    public static Result Failure(Error error) => new(false, error);
    public static Result<T> Failure<T>(Error error) => new(default, false, error);
    
    public static implicit operator Result(Error error) => Failure(error);
}

public sealed class Result<T> : Result
{
    private readonly T? _value;

    internal Result(T? value, bool isSuccess, Error? error) : base(isSuccess, error)
    {
        _value = value;
    }
    
    public T Value => IsSuccess ? _value! : throw new InvalidOperationException("Value is not available for failed result.");
    
    public static implicit operator Result<T>(T value) => Success(value);
    public static implicit operator Result<T>(Error error) => Failure<T>(error);
}