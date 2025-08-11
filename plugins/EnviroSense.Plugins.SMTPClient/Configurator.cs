using EnviroSense.Plugins.SMTPClient.Impl;
using EnviroSense.Repositories.Clients;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EnviroSense.Plugins.SMTPClient;

public static class Configurator
{
    public static void AddSMTPClient(this IServiceCollection serviceCollection, IConfigurationSection configurationSection)
    {
        serviceCollection.Configure<EmailSetings>(configurationSection);
        serviceCollection.AddScoped<IEmailClient, EmailClient>();
    }
}
