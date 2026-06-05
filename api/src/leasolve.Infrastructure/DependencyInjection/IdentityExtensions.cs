using leasolve.Application.Abstractions;
using leasolve.Domain.Users;
using leasolve.Infrastructure.Database;
using leasolve.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace leasolve.Infrastructure.DependencyInjection;

internal static class IdentityExtensions
{
    internal static IServiceCollection AddIdentityServices(this IServiceCollection services)
    {
        services.AddIdentity<User, IdentityRole<long>>()
            .AddEntityFrameworkStores<DatabaseContext>();

        services.AddScoped<IUserService, UserService>();
        return services;
    }
}