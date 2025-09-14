using EnviroSense.API.Authentication;
using EnviroSense.API.Configuration;
using EnviroSense.API.Filters;
using EnviroSense.Application.Authentication;
using EnviroSense.Application.Authorization;
using Microsoft.Extensions.Configuration;

namespace EnviroSense.API;

public static class Configurator
{
    public static void AddAPIServices(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        // Add services to the container.
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        serviceCollection.AddOpenApi();

        // Configure strongly-typed settings
        serviceCollection.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

        // Add app related services.
        serviceCollection.AddControllers(opts =>
        {
            opts.Filters.Add<HandleGenericErrors>();
        });
        serviceCollection.AddHttpContextAccessor();
        serviceCollection.AddScoped<IAuthenticationContext, MixedAuthentication>();
        serviceCollection.AddScoped<ITokenAuthentication, MixedAuthentication>();
        
        serviceCollection.AddCors(options =>
        {
            options.AddPolicy(name: "Base",
                policy  =>
                {
                    policy.AllowAnyHeader()
                        .AllowAnyMethod().AllowAnyOrigin();
                });
        });
    }

}
