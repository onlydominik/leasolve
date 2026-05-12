using leasolve.Infrastructure.Database;
using leasolve.Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace leasolve.Infrastructure.DependencyInjection;

internal static class DatabaseExtensions
{
    internal static IServiceCollection AddDatabaseServices(this IServiceCollection services, IConfiguration configuration)
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
    
    internal static void UseDatabaseServices(this IServiceProvider service)
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