using EnviroSense.Application.Emailing;
using EnviroSense.Domain.Emailing;
using RazorLight;

namespace EnviroSense.Plugins.RazorEmailing.Renders;

public abstract class BaseRender<T> : IEmailRenderer<T>
    where T : BaseEmail
{
    private readonly RazorLightEngine _razorLightEngine;

    protected BaseRender(RazorLightEngine razorLightEngine)
    {
        _razorLightEngine = razorLightEngine;
    }

    public abstract string TemplateName { get; }

    public virtual object PrepareModel(T input)
    {
        return input;
    }

    public async Task<string> Render(T model)
    {
        var fullTemplatePath = $"EnviroSense.Plugins.RazorEmailing.EmailTemplates.{TemplateName}";
        var result = await _razorLightEngine.CompileRenderAsync(fullTemplatePath, model);

        return result;
    }
}
