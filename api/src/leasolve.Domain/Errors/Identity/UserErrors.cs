using leasolve.Domain.Abstractions;

namespace leasolve.Domain.Errors.Identity;

public static class UserErrors
{
    public static readonly Error InvalidFirstName = new(ErrorType.Validation, "UserErrors.InvalidFirstName", "First name cannot be empty or whitespace.");
    public static readonly Error InvalidLastName = new(ErrorType.Validation, "UserErrors.InvalidLastName", "Last name cannot be empty or whitespace.");
    public static readonly Error InvalidEmail = new(ErrorType.Validation, "UserErrors.InvalidEmail", "Email cannot be empty or whitespace.");
    
    public static readonly Error FirstNameTooLong = new(ErrorType.Validation, "UserErrors.FirstNameTooLong", "First name exceeds the maximum length.");
    public static readonly Error LastNameTooLong = new(ErrorType.Validation, "UserErrors.LastNameTooLong", "Last name exceeds the maximum length.");
    public static readonly Error EmailTooLong = new(ErrorType.Validation, "UserErrors.EmailTooLong", "Email exceeds the maximum length.");
}