using leasolve.Domain.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace leasolve.API.Results;

public static class ApiResults
{
    public static ActionResult<T> Problem<T>(Result<T> result)
    {
        if (result.IsSuccess)
        {
            throw new InvalidOperationException();
        }

        var statusCode = GetStatusCode(result.Error.Type);
        var problemDetails = new ProblemDetails
        {
            Title = GetTitle(result.Error),
            Status = GetStatusCode(result.Error.Type),
            Detail = GetDetail(result.Error)
        };

        return new ObjectResult(problemDetails) { StatusCode = statusCode };
        
        static string GetTitle(Error error) => error.Type switch
        {
            ErrorType.Validation => error.Code,
            ErrorType.Problem => error.Code,
            ErrorType.Unauthorized => error.Code,
            ErrorType.Forbidden => error.Code,
            ErrorType.NotFound => error.Code,
            ErrorType.Conflict => error.Code,
            _ => "Server failure."
        };

        static string GetDetail(Error error) => error.Type switch
        {
            ErrorType.Validation => error.Details,
            ErrorType.Problem => error.Details,
            ErrorType.Unauthorized => error.Details,
            ErrorType.Forbidden => error.Details,
            ErrorType.NotFound => error.Details,
            ErrorType.Conflict => error.Details,
            _ => "An unexpected error occurred."
        };
        
        static int GetStatusCode(ErrorType errorType) => errorType switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.Problem => StatusCodes.Status400BadRequest,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorType.Forbidden => StatusCodes.Status403Forbidden,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };

        // static Dictionary<string, object>? GetErrors(Result result)
        // {
        //     if (result.Error is not ValidationError validationError)
        //     {
        //         return null;
        //     }
        //
        //     return new Dictionary<string, object?>
        //     {
        //         { "errors", validationError.Errors } 
        //     };
        // }
    }
}