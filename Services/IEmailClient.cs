namespace EnviroSense.Web.Services;

public interface IEmailClient
{
    Task SendMail(string title, string body, string email);
}
