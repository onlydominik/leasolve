using leasolve.Domain.Abstractions;
using leasolve.Domain.Core;
using System.Text.RegularExpressions;

namespace leasolve.Domain.ValueObjects;

public sealed partial class Email : ValueObject
{
    public const int MaxLength = 256;

    [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase)]
    private static partial Regex EmailRegex();
    
    public string Value { get; private init; }

    private Email(string email)
    {
        Value = email;
    }

    public static Result<Email> Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return ValueObjectErrors.Email.Empty;

        string trimmedEmail = email.Trim();
        
        if (trimmedEmail.Length > MaxLength)
            return ValueObjectErrors.Email.TooLong(MaxLength, trimmedEmail.Length);

        if (!EmailRegex().IsMatch(trimmedEmail))
            return ValueObjectErrors.Email.InvalidFormat;

#pragma warning disable CA1308
        trimmedEmail = trimmedEmail.ToLowerInvariant();
#pragma warning restore CA1308

        return new Email(trimmedEmail);
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
    
    public override string ToString() => Value;
}