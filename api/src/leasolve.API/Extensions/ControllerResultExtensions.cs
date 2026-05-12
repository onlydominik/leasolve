using leasolve.Domain.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace leasolve.API.Extensions;

public static class ControllerResultExtensions
{
    public static Task<ActionResult<T>> MatchAsync<T>(
        this ControllerBase controller,
        Result<T> result,
        Func<Result<T>, ActionResult<T>> onFailure)
    {
        ArgumentNullException.ThrowIfNull(controller);
        return Task.FromResult(result.IsSuccess ? controller.Ok(result.Value) : onFailure(result));
    }
}