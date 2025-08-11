using EnviroSense.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EnviroSense.Application;

public static class Configurator
{
    public static void AddApplicationServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IMeasurementService, MeasurementService>();
        serviceCollection.AddScoped<IDeviceService, DeviceService>();
        serviceCollection.AddScoped<IAccessService, AccessService>();
        serviceCollection.AddScoped<IAccountService, AccountService>();
    }
}
