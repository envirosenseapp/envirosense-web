using EnviroSense.API.Filters;

namespace EnviroSense.API;

public static class Configurator
{
    public static void AddAPIServices(this IServiceCollection serviceCollection)
    {
        // Add services to the container.
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        serviceCollection.AddOpenApi();

        // Add app related services.
        serviceCollection.AddControllers(opts =>
        {
            opts.Filters.Add<HandleInternalServiceErrors>();
        });
        serviceCollection.AddHttpContextAccessor();
    }

}
