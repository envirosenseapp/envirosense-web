using EnviroSense.Domain.Emailing;
using Microsoft.Extensions.Logging;
using RazorLight;

namespace EnviroSense.Plugins.RazorEmailing.Renders;

public class SendSignedUpEmailRender : BaseRender<SendSignedUpEmailPayload>
{
    public SendSignedUpEmailRender(
        RazorLightEngine razorLightEngine,
        ILogger<BaseRender<SendSignedUpEmailPayload>> logger
    ) : base(razorLightEngine, logger)
    {
    }
    protected override string TemplateName { get; } = "SendSignedUpEmail.cshtml";
    protected override string Title { get; } = "You are successfully signed up.";
}
