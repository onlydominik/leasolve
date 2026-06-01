using leasolve.Domain.Abstractions;

namespace leasolve.Domain.Users;

public static class UsersErrors
{
        public static readonly Error EmailAlreadyExists = Error.Conflict("User.EmailAlreadyExists", "A user with this email address already exists.");
        
        public static readonly Error RegistrationFailed = Error.Conflict("User.RegistrationFailed", "User registration failed. Please check your details and try again.");
        
        public static readonly Error FirstNameEmpty = Error.Validation("User.FirstNameEmpty", "First name cannot be empty or whitespace.");
        
        public static Error FirstNameTooLong(int maxLength, int actualLength) => Error.Validation("User.FirstNameTooLong", $"First name cannot exceed {maxLength} characters. Current length: {actualLength}.");
        
        public static readonly Error LastNameEmpty = Error.Validation("User.LastNameEmpty", "Last name cannot be empty or whitespace.");
        
        public static Error LastNameTooLong(int maxLength, int actualLength) => Error.Validation("User.LastNameTooLong", $"Last name cannot exceed {maxLength} characters. Current length: {actualLength}.");
        
        public static readonly Error RoleNotFound = Error.NotFound("User.RoleNotFound", "The specified role was not found.");
        
        public static readonly Error RoleAssignmentFailed = Error.Conflict("User.RoleAssignmentFailed", "Unable to assign the requested role to the user.");
}

