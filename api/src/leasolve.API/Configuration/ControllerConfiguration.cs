namespace leasolve.API.Configuration;

public static class ControllerConfiguration
{
    public static IServiceCollection AddCustomControllers(this IServiceCollection services)
    {
        services.AddControllers();
        return services;
    }

    public static WebApplication UseCustomControllers(this WebApplication app)
    {
        app.MapControllers();
        return app;
    }
}