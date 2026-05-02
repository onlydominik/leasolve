namespace leasolve.Domain.Abstractions;

public enum ErrorType
{
    None,
    Validation,
    Problem,
    Conflict,
    NotFound,
    Unauthorized,
    Forbidden
}