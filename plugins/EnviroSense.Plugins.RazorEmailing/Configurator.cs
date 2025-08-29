using System.Reflection;
using EnviroSense.Application.Emailing;
using EnviroSense.Domain.Emailing;
using EnviroSense.Plugins.RazorEmailing.Renders;
using Microsoft.Extensions.DependencyInjection;
using RazorLight;

namespace EnviroSense.Plugins.RazorEmailing;

public static class Configurator
{
    public static void AddRazorEmailing(this IServiceCollection serviceCollection)
    {
        // renders
        serviceCollection.AddSingleton<IEmailRenderer<SendSignedInEmail>, SendSignedInEmailRender>();

        var resources = Assembly.GetExecutingAssembly().GetManifestResourceNames();

        // engine
        var razor = new RazorLightEngineBuilder()
            .UseEmbeddedResourcesProject(Assembly.GetExecutingAssembly())
            .UseMemoryCachingProvider()
            .Build();

        serviceCollection.AddSingleton(razor);
    }
}
