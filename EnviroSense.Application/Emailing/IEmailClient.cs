namespace EnviroSense.Application.Emailing;

public interface IEmailClient
{
    Task SendMail(string title, string body, string email);
}
