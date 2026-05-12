using leasolve.Infrastructure.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace leasolve.Infrastructure;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Infrastructure specific DI
        services.AddDatabaseServices(configuration);
        services.AddIdentityServices();
        services.AddDataSeederServices();
        
        return services;
    }

    public static void UseInfrastructure(this IServiceProvider service)
    {
        // Infrastructure specific DI
        service.UseDatabaseServices();
        service.UseDataSeederServices();
    }
}
[SuppressMessage("Major Code Smell", "S2094", Justification = "Marker")]
public sealed class AssemblyMarker {}