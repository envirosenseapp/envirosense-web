using EnviroSense.Application.Emailing;
using EnviroSense.Domain.Emailing;
using Microsoft.Extensions.Logging;
using RazorLight;

namespace EnviroSense.Plugins.RazorEmailing.Renders;

public class SendSignedInEmailRender : BaseRender<SendSignedInEmailPayload>
{
    public SendSignedInEmailRender(
        RazorLightEngine razorLightEngine,
ILogger<BaseRender<SendSignedInEmailPayload>> logger
    ) : base(razorLightEngine, logger)
    {
    }

    protected override string TemplateName { get; } = "SendSignedInEmail.cshtml";
    protected override string Title { get; } = "You are successfully signed in.";
}
