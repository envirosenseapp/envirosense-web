namespace EnviroSense.Repositories.Clients;

public interface IEmailClient
{
    Task SendMail(string title, string body, string email, bool isHtml);
}
