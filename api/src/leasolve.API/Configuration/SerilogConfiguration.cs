using Serilog;

namespace leasolve.API.Configuration;

public static class SerilogConfiguration
{
    public static WebApplicationBuilder AddCustomSerilog(this WebApplicationBuilder services)
    {
        services.Host.UseSerilog((context, configuration) =>
            configuration.ReadFrom.Configuration(context.Configuration));

        return services;
    }
}