using EnviroSense.Domain.Emailing;
using Microsoft.Extensions.Logging;
using RazorLight;

namespace EnviroSense.Plugins.RazorEmailing.Renders;

public class SendPasswordResetedNotificationEmailRender : BaseRender<SendPasswordResetedNotificationEmailPayload>
{
    public SendPasswordResetedNotificationEmailRender(
        RazorLightEngine razorLightEngine,
        ILogger<BaseRender<SendPasswordResetedNotificationEmailPayload>> logger
    ) : base(razorLightEngine, logger)
    {
    }
    protected override string TemplateName { get; } = "SendPasswordResetNotificationEmail.cshtml";
    protected override string Title { get; } = "Password for EnviroSense Has Been Reseted";
}
