using leasolve.API.Extensions;
using leasolve.API.Settings;
using Microsoft.Extensions.Options;
using Scalar.AspNetCore;

namespace leasolve.API.Configuration;

public static class OpenApiDocumentationConfiguration
{
    public static IServiceCollection AddCustomOpenApiDocumentation(this IServiceCollection services)
    {
        services.AddSettings<ScalarSettings>();
        services.AddOpenApi();

        return services;
    }

    public static WebApplication UseCustomOpenApiDocumentation(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment())
        {
            return app;
        }

        app.MapOpenApi();

        var scalarSettings = app.Services.GetRequiredService<IOptions<ScalarSettings>>().Value;

        app.MapScalarApiReference("/docs", options =>
        {
            options.WithTitle(scalarSettings.Title!);
        });

        return app;
    }
}