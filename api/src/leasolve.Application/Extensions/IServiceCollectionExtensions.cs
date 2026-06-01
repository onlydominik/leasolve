using leasolve.Application.Settings.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace leasolve.Application.Extensions;

public static class IServiceCollectionExtensions
{
    // TODO: At the moment, AddSettings is in Application,
    // we need to think about moving it to a higher layer so that the infra and API have access
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