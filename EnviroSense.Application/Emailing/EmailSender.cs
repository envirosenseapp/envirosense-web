using System.Text;
using EnviroSense.Domain.Emailing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EnviroSense.Application.Emailing;

public class EmailSender : IEmailSender
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IEmailClient _emailClient;
    private readonly ILogger<EmailSender> _logger;

    public EmailSender(
        IServiceProvider serviceProvider,
        IEmailClient emailClient,
        ILogger<EmailSender> logger)
    {
        _serviceProvider = serviceProvider;
        _emailClient = emailClient;
        _logger = logger;
    }

    public async Task SendEmailAsync<T>(string email, T payload) where T : BaseEmail
    {
        var renderer = _serviceProvider.GetRequiredService<IEmailRenderer<T>>();

        _logger.LogDebug("Rendering email body");
        var renderedEmail = await renderer.Render(payload);

        _logger.LogDebug("Sending email");
        await _emailClient.SendMail(
            renderedEmail.Title,
            renderedEmail.Body,
            email
            );

        _logger.LogInformation("Email sent");
    }
}
