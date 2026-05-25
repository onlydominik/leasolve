using leasolve.Domain.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace leasolve.Domain.Identity;

public sealed class User : IdentityUser<long>
{
    // TODO: move consts to validator in application maybe
    public const int FirstNameMaxLength = 100;
    public const int LastNameMaxLength = 100;
    
    private User() { }

    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;


    public static Result<User> Create(string email, string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            return IdentityErrors.User.FirstNameEmpty;
                
        var trimmedFirstName = firstName.Trim();
        
        if (trimmedFirstName.Length > FirstNameMaxLength)    
            return IdentityErrors.User.FirstNameTooLong(FirstNameMaxLength, trimmedFirstName.Length);
        
        if (string.IsNullOrWhiteSpace(lastName))
            return IdentityErrors.User.LastNameEmpty;
                
        var trimmedLastName = firstName.Trim();
        
        if (lastName.Trim().Length > LastNameMaxLength)    
            return IdentityErrors.User.LastNameTooLong(LastNameMaxLength, trimmedLastName.Length);
        
        var emailResult = ValueObjects.Email.Create(email);

        if (emailResult.IsFailure)
            return emailResult.Error;
        
        return new User { FirstName = firstName, LastName = lastName, Email = emailResult.Value.ToString(), UserName = emailResult.Value.ToString() };
    }
}