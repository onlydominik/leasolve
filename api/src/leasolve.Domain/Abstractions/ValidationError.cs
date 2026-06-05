namespace leasolve.Domain.Abstractions;

public sealed class ValidationError : Error
{
    public IReadOnlyDictionary<string, string[]> Errors { get; }

    public ValidationError(
        string code,
        string details,
        IReadOnlyDictionary<string, string[]> errors) 
        : base(ErrorType.Validation, code, details)
    {
        Errors = errors;
    }
}