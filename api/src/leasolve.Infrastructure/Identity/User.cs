using leasolve.Domain.Abstractions;
using leasolve.Domain.Extensions;
using leasolve.Domain.Errors.Identity;
using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;

namespace leasolve.Infrastructure.Identity;

public sealed partial class User : IdentityUser<long>
{
    // TODO: move consts to validator in application maybe
    public const int FirstNameMaxLength = 100;
    public const int LastNameMaxLength = 100;
    public const int EmailMaxLength = 256;

    // TODO: VO for email if it appears somewhere other than IdentityUser, not using VO for email in IdentityUser
    [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase)]
    private static partial Regex EmailRegex();

    
    private User() { }

    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;


    public static Result<User> Create(string email, string firstName, string lastName)
    {
        var user = new User();

        return Result.Success(user)
            .Bind(u => u.SetEmail(email))
            .Bind(u => u.SetFirstName(firstName))
            .Bind(u => u.SetLastName(lastName))
            .Bind(u => u.SetUserName());
    }

    public Result<User> UpdateProfile(string firstName, string lastName)
    {
        return Result.Success(this)
            .Bind(u => u.SetFirstName(firstName))
            .Bind(u => u.SetLastName(lastName));
    }

    
    private Result<User> SetEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return UserErrors.InvalidEmail;

        if (email.Trim().Length > EmailMaxLength)
            return UserErrors.EmailTooLong;

        if (!EmailRegex().IsMatch(email.Trim()))
            return UserErrors.InvalidEmail;

#pragma warning disable CA1308
        Email = email.Trim().ToLowerInvariant();
#pragma warning restore CA1308
        return this;
    }

    private Result<User> SetFirstName(string firstName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            return UserErrors.InvalidFirstName;

        if (firstName.Trim().Length > FirstNameMaxLength)
            return UserErrors.FirstNameTooLong;

        FirstName = firstName.Trim();
        return this;
    }

    private Result<User> SetLastName(string lastName)
    {
        if (string.IsNullOrWhiteSpace(lastName))
            return UserErrors.InvalidLastName;

        if (lastName.Trim().Length > LastNameMaxLength)
            return UserErrors.LastNameTooLong;

        LastName = lastName.Trim();
        return this;
    }
    
    private Result<User> SetUserName()
    {
        UserName = Email;
        return this;
    }
}