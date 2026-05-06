using leasolve.Infrastructure.Abstractions.Settings;
using leasolve.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Diagnostics.CodeAnalysis;

namespace leasolve.Infrastructure;

public static class InfrastructureServiceCollectionExtensions
{
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
    }
}
[SuppressMessage("Major Code Smell", "S2094", Justification = "Marker")]
public sealed class AssemblyMarker {}