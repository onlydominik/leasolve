using leasolve.Domain.Abstractions;

namespace leasolve.Domain.ValueObjects;

public static class ValueObjectErrors
{
    public static class Email
    {
        public static readonly Error Empty = Error.Validation("Email.Empty", "Email cannot be empty or whitespace.");
        
        public static readonly Error InvalidFormat = Error.Validation("Email.InvalidFormat", "Email format is invalid.");
        
        public static Error TooLong(int maxLength, int actualLength) => Error.Validation("Email.TooLong", $"Email cannot exceed {maxLength} characters. Current length: {actualLength}.");
    }
}