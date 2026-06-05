namespace leasolve.Domain.Abstractions;

public class Error
{
    public ErrorType Type { get; }
    public string Code { get; }
    public string Details { get; }

    protected Error(ErrorType type, string code, string details)
    {
        Type = type;
        Code = code;
        Details = details;
    }
    
    public static Error Validation(string code, string details) => new(ErrorType.Validation, code, details);
    
    public static Error NotFound(string code, string details) => new(ErrorType.NotFound, code, details);
    
    public static Error Conflict(string code, string details) => new(ErrorType.Conflict, code, details);
    
    public static Error Unauthorized(string code, string details) => new(ErrorType.Unauthorized, code, details);
    
    public static Error Forbidden(string code, string details) => new(ErrorType.Forbidden, code, details);
}