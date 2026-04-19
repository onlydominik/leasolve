using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace leasolve.Application;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssemblyContaining<AssemblyMarker>();
        });

        services.AddValidatorsFromAssemblyContaining<AssemblyMarker>();
        return services;
    }
}

[SuppressMessage("Major Code Smell", "S2094", Justification = "Marker")]
public sealed class AssemblyMarker {}