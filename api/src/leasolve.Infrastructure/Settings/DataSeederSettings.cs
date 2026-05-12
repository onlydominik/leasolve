using leasolve.Application.Settings.Interfaces;

namespace leasolve.Infrastructure.Settings;

public sealed class DataSeederSettings : IValidatedSettings
{
    public bool SeedData { get; init; }
    public SeedUserData? AdminUser { get; init; }
    
    public bool IsValid()
    {
        if (!SeedData) return true;
        if (AdminUser is null) return false;
        
        if (string.IsNullOrWhiteSpace(AdminUser.Email) 
            || string.IsNullOrWhiteSpace(AdminUser.Password)) return false;
        
        return true;
    }
}

public sealed class SeedUserData
{
    public string? Email { get; init; }
    public string? Password { get; init; }
}