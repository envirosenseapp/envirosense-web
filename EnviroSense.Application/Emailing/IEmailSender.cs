using EnviroSense.Domain.Emailing;

namespace EnviroSense.Application.Emailing;

public interface IEmailSender
{
    Task SendEmailAsync<T>(string email, T payload) where T : BaseEmail;
}
