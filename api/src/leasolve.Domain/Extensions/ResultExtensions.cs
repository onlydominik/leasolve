using leasolve.Domain.Abstractions;

namespace leasolve.Domain.Extensions;

public static class ResultExtensions
{
    public static Result<TOut> Bind<TIn, TOut>(
        this Result<TIn> result,
        Func<TIn, Result<TOut>> binder)
    {
        return result.IsSuccess 
            ? binder(result.Value)
            : Result.Failure<TOut>(result.Error);
    }
}