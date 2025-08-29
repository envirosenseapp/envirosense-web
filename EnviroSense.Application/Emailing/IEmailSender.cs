using EnviroSense.Domain.Emailing;

namespace EnviroSense.Application.Emailing;

public interface IEmailSender
{
    Task SendEmailAsync<T>(T payload) where T : BaseEmail;
}
