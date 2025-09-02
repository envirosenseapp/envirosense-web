using EnviroSense.Domain.Emailing;
using Microsoft.Extensions.Logging;
using RazorLight;

namespace EnviroSense.Plugins.RazorEmailing.Renders;

public class SendPasswordResetLinkEmailRender: BaseRender<SendPasswordResetLinkEmailPayload>
{
    public SendPasswordResetLinkEmailRender(
        RazorLightEngine razorLightEngine,
        ILogger<BaseRender<SendPasswordResetLinkEmailPayload>> logger
    ) : base(razorLightEngine, logger)
    {
    }
    protected override string TemplateName { get; } = "SendPasswordResetLinkEmail.cshtml";
    protected override string Title { get; } = "Password reset Link";
}
