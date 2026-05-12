using leasolve.Application.Settings.Interfaces;

namespace leasolve.Infrastructure.Settings;

public sealed class DatabaseSettings : IValidatedSettings
{
    public string? ConnectionString { get; init; }
    public bool AutoMigrate { get; init; }
    
    public bool IsValid()
    {
        if (string.IsNullOrWhiteSpace(ConnectionString)) return false;

        return true;
    }
}