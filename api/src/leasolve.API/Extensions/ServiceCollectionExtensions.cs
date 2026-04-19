using leasolve.Application.Settings.Interfaces;

namespace leasolve.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSettings<TSettings>(this IServiceCollection services)
        where TSettings : class, IValidatedSettings
    {
        ArgumentNullException.ThrowIfNull(services);

        var sectionName = typeof(TSettings).Name;

        return services.AddOptions<TSettings>()
                       .BindConfiguration(sectionName)
                       .Validate(settings => settings.IsValid(), $"{sectionName} is not valid.")
                       .ValidateOnStart()
                       .Services;
    }
}