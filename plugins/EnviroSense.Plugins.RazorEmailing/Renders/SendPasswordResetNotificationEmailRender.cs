using EnviroSense.Domain.Emailing;
using Microsoft.Extensions.Logging;
using RazorLight;

namespace EnviroSense.Plugins.RazorEmailing.Renders;

public class SendPasswordResetNotificationEmailRender : BaseRender<SendPasswordResetNotificationEmailPayload>
{
    public SendPasswordResetNotificationEmailRender(
        RazorLightEngine razorLightEngine,
        ILogger<BaseRender<SendPasswordResetNotificationEmailPayload>> logger
    ) : base(razorLightEngine, logger)
    {
    }
    protected override string TemplateName { get; } = "SendPasswordResetNotificationEmail.cshtml";
    protected override string Title { get; } = "Password for EnviroSense Has Been Reset";
}
