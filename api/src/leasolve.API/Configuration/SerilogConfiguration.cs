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

    public static IApplicationBuilder UseCustomSerilog(this IApplicationBuilder app)
    {
        app.UseSerilogRequestLogging(config =>
        {
            config.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
            {
                diagnosticContext.Set("User-Agent", httpContext.Request.Headers.UserAgent);
                diagnosticContext.Set("Accept-Language", httpContext.Request.Headers.AcceptLanguage);
            };
        });

        return app;
    }
}