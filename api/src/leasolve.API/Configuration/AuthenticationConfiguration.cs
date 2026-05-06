using leasolve.Infrastructure.Database;
using leasolve.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace leasolve.API.Configuration;

public static class AuthenticationConfiguration
{
    public static IServiceCollection AddCustomAuthentication(this IServiceCollection services)
    {
        services.AddIdentity<User, IdentityRole<long>>()
            .AddEntityFrameworkStores<DatabaseContext>();
        
        return services;
    }
}