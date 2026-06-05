using leasolve.Domain.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace leasolve.API.Results;

public static class ApiResults
{
    public static ActionResult<T> Problem<T>(Result<T> result)
    {
        if (result.IsSuccess || result.Error is null)
        {
            throw new InvalidOperationException();
        }

        if (result.Error is ValidationError validationError)
        {
            var validationProblem = new ValidationProblemDetails(
                validationError.Errors.ToDictionary(kv => kv.Key, kv => kv.Value))
            {
                Title = validationError.Code,
                Detail = validationError.Details,
                Status = StatusCodes.Status400BadRequest
            };

            return new ObjectResult(validationProblem)
            {
                StatusCode = StatusCodes.Status400BadRequest
            };
        }
        
        var statusCode = GetStatusCode(result.Error.Type);
        var problemDetails = new ProblemDetails
        {
            Title = result.Error.Code,
            Status = statusCode,
            Detail = result.Error.Details
        };

        return new ObjectResult(problemDetails) { StatusCode = statusCode };
        
        static int GetStatusCode(ErrorType errorType) => errorType switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorType.Forbidden => StatusCodes.Status403Forbidden,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };
    }
}