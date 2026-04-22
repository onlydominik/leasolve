using leasolve.API.Exceptions;

namespace leasolve.API.Configuration;

public static class ExceptionHandlersConfiguration
{
    public static IServiceCollection AddCustomExceptionHandlers(this IServiceCollection services)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();
        
        services.AddProblemDetails();
        
        return services;
    }
    
    public static IApplicationBuilder UseCustomExceptionHandlers(this IApplicationBuilder app)
    {
        app.UseExceptionHandler();

        return app;
    }
}