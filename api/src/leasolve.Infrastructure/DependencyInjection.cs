using leasolve.Application.Extensions;
using leasolve.Infrastructure.Abstractions.Settings;
using leasolve.Infrastructure.Database;
using leasolve.Infrastructure.Identity;
using leasolve.Infrastructure.Services;
using leasolve.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Diagnostics.CodeAnalysis;

namespace leasolve.Infrastructure;

public static class InfrastructureServiceCollectionExtensions
{
    // NOTE: in the future we may need to create specific DI files in infra and add them here if it grows
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var databaseSettingsSection = configuration.GetSection(nameof(DatabaseSettings));
        services.Configure<DatabaseSettings>(databaseSettingsSection);

        var databaseSettings = databaseSettingsSection.Get<DatabaseSettings>();
        var connectionString = databaseSettings?.ConnectionString ?? 
                               throw new InvalidOperationException("Database connection string is not set.");

        services.AddDbContext<DatabaseContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        services.AddIdentity<User, IdentityRole<long>>()
            .AddEntityFrameworkStores<DatabaseContext>();
        
        // Settings
        services.AddSettings<DataSeederSettings>();
        
        // Services
        // TODO: if many use scrutor
        services.AddScoped<IDataSeederService, DataSeederService>();
        
        return services;
    }

    public static void UseInfrastructure(this IServiceProvider service)
    {
        using var scope = service.CreateScope();
        var databaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
        var databaseSettings = scope.ServiceProvider.GetRequiredService<IOptions<DatabaseSettings>>().Value;

        if (databaseSettings.AutoMigrate)
        {
            databaseContext.Database.Migrate();
        }
        
        var dataSeederService = scope.ServiceProvider.GetRequiredService<IDataSeederService>();
        dataSeederService.Seed().Wait();
    }
}
[SuppressMessage("Major Code Smell", "S2094", Justification = "Marker")]
public sealed class AssemblyMarker {}