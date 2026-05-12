using leasolve.Application.Extensions;
using leasolve.Infrastructure.Services;
using leasolve.Infrastructure.Services.Interfaces;
using leasolve.Infrastructure.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace leasolve.Infrastructure.DependencyInjection;

internal static class DataSeederExtensions
{
    internal static IServiceCollection AddDataSeederServices(this IServiceCollection services)
    {
        services.AddSettings<DataSeederSettings>();
        
        // TODO: if many in infra - use scrutor
        services.AddScoped<IDataSeederService, DataSeederService>();
        
        return services;
    }
    
    internal static void UseDataSeederServices(this IServiceProvider service)
    {
        using var scope = service.CreateScope();
        
        var dataSeederService = scope.ServiceProvider.GetRequiredService<IDataSeederService>();
        dataSeederService.Seed().Wait();
    }
}