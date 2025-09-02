using EnviroSense.Domain.Emailing;
using Microsoft.Extensions.Logging;
using RazorLight;

namespace EnviroSense.Plugins.RazorEmailing.Renders;

public class SendPasswordChangedNotificationEmailRender: BaseRender<SendPasswordChangedNotificationEmailPayload>
{
    public SendPasswordChangedNotificationEmailRender(
        RazorLightEngine razorLightEngine,
        ILogger<BaseRender<SendPasswordChangedNotificationEmailPayload>> logger
    ) : base(razorLightEngine, logger)
    {
    }
    protected override string TemplateName { get; } = "SendPasswordChangedNotificationEmail.cshtml";
    protected override string Title { get; } = "Password for EnviroSense Has Been Changed..";
}
