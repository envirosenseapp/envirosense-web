using EnviroSense.Application.Emailing;
using EnviroSense.Domain.Emailing;
using RazorLight;

namespace EnviroSense.Plugins.RazorEmailing.Renders;

public class SendSignedInEmailRender : BaseRender<SendSignedInEmail>
{
    public SendSignedInEmailRender(RazorLightEngine razorLightEngine) : base(razorLightEngine)
    {
    }

    public override string TemplateName { get; } = "SendSignedInEmail.cshtml";
}
