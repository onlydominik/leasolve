using Microsoft.AspNetCore.Identity;

namespace leasolve.Infrastructure.Identity;

public sealed class User : IdentityUser<long>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}