using Microsoft.AspNetCore.Mvc.Rendering;

namespace EnviroSense.Web;

public static class HtmlAuthExtensions
{
    public static bool IsLoggedIn<T>(this IHtmlHelper<T> html)
    {
        var session = html.ViewContext.HttpContext.Session;
        var accountId = session.GetString("authenticated_account_id");

        return !string.IsNullOrEmpty(accountId);
    }

    public static bool IsLoggedOut<T>(this IHtmlHelper<T> html)
    {
        var session = html.ViewContext.HttpContext.Session;
        var accountId = session.GetString("authenticated_account_id");

        return string.IsNullOrEmpty(accountId);
    }
}
