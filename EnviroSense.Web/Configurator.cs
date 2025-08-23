using EnviroSense.Application.Authentication;
using EnviroSense.Web.Authentication;
using EnviroSense.Web.Filters;

namespace EnviroSense.Web;

public static class Configurator
{
    public static void AddWebServices(this IServiceCollection serviceCollection)
    {
        // add authentication services
        serviceCollection.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(30);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });
        serviceCollection.AddScoped<IAuthenticationRetriever, SessionAuthentication>();
        serviceCollection.AddScoped<ISessionAuthentication, SessionAuthentication>();

        // add mvc related
        serviceCollection.AddControllersWithViews();
        serviceCollection.AddHttpContextAccessor();
        var mvcBuilder = serviceCollection.AddControllersWithViews(opts =>
        {
            opts.Filters.Add<AccessTrackingFilter>();
        });

#if DEBUG
        mvcBuilder.AddRazorRuntimeCompilation();
#endif
    }

}
