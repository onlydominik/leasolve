using leasolve.Domain.Abstractions;

namespace leasolve.Domain.Identity;

public static class IdentityErrors
{
    public static class User
    {
        public static readonly Error FirstNameEmpty = Error.Validation("User.FirstNameEmpty", "First name cannot be empty or whitespace.");
        
        public static Error FirstNameTooLong(int maxLength, int actualLength) => Error.Validation("User.FirstNameTooLong", $"First name cannot exceed {maxLength} characters. Current length: {actualLength}.");
        
        public static readonly Error LastNameEmpty = Error.Validation("User.LastNameEmpty", "Last name cannot be empty or whitespace.");
        
        public static Error LastNameTooLong(int maxLength, int actualLength) => Error.Validation("User.LastNameTooLong", $"Last name cannot exceed {maxLength} characters. Current length: {actualLength}.");
    }
}