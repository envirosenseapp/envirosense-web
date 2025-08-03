using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
namespace EnviroSense.Web.Services;

public class EmailClient : IEmailClient
{
    private readonly EmailSetings _settings;
    private readonly ILogger<EmailClient> _logger;

    public EmailClient(IOptions<EmailSetings> settings, ILogger<EmailClient> logger)
    {
        _settings = settings.Value;
        _logger = logger;
    }

    public async Task SendMail(string title, string body, string email)
    {
        var message = new MimeMessage();
        message.From.Add(MailboxAddress.Parse(_settings.From));
        message.To.Add(MailboxAddress.Parse(email));
        message.Subject = title;

        message.Body = new TextPart("plain") { Text = body };

        using var client = new SmtpClient();
        await client.ConnectAsync(_settings.Host, _settings.Port, _settings.UseSsl);
        await client.AuthenticateAsync(_settings.Username, _settings.Password);
        await client.SendAsync(message);
        await client.DisconnectAsync(true);

        _logger.LogInformation("Email sent to {Email} with subject: {Subject}", email, title);
    }
}
