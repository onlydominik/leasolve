using leasolve.Application.Settings.Interfaces;

namespace leasolve.API.Settings;

public sealed class ScalarSettings : IValidatedSettings
{
    public string? Title { get; init; }

    public bool IsValid()
    {
        if (string.IsNullOrWhiteSpace(Title)) return false;

        return true;
    }
}