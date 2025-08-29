using EnviroSense.Application.Algorithms;
using EnviroSense.Application.Authentication;
using EnviroSense.Application.Authorization;
using EnviroSense.Application.Authorization.AccessRules;
using EnviroSense.Application.Emailing;
using EnviroSense.Application.Services;
using EnviroSense.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace EnviroSense.Application;

public static class Configurator
{
    public static void AddApplicationServices(this IServiceCollection serviceCollection)
    {
        // services
        serviceCollection.AddScoped<IMeasurementService, MeasurementService>();
        serviceCollection.AddScoped<IDeviceService, DeviceService>();
        serviceCollection.AddScoped<IAccessService, AccessService>();
        serviceCollection.AddScoped<IAccountService, AccountService>();
        serviceCollection.AddScoped<IAccountPasswordResetService, AccountPasswordResetService>();
        serviceCollection.AddScoped<IApiKeyService, ApiKeyService>();

        // authorization
        serviceCollection.AddScoped<IAccessRule<Device>, DeviceAccessRule>();
        serviceCollection.AddScoped<IAccessRule<Measurement>, MeasurementAccessRule>();
        serviceCollection.AddScoped<IAccessRule<ApiKey>, DeviceApiKeyAccessRule>();
        serviceCollection.AddScoped<IAuthorizationResolver, AuthorizationResolver>();

        // other
        serviceCollection.AddSingleton<IApiKeyGenerator, GuidApiKeyGenerator>();
        serviceCollection.AddSingleton<IEmailSender, EmailSender>();
    }
}
