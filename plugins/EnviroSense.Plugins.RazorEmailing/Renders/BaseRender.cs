using System.Diagnostics;
using EnviroSense.Application.Emailing;
using EnviroSense.Domain.Emailing;
using Microsoft.Extensions.Logging;
using RazorLight;

namespace EnviroSense.Plugins.RazorEmailing.Renders;

public abstract class BaseRender<T> : IEmailRenderer<T>
    where T : BaseEmail
{
    private readonly RazorLightEngine _razorLightEngine;
    private readonly ILogger<BaseRender<T>> _logger;

    protected BaseRender(RazorLightEngine razorLightEngine, ILogger<BaseRender<T>> logger)
    {
        _razorLightEngine = razorLightEngine;
        _logger = logger;
    }

    protected abstract string TemplateName { get; }

    protected abstract string Title { get; }

    public virtual object PrepareModel(T input)
    {
        return input;
    }

    public async Task<RenderedEmail> Render(T model)
    {
        var fullTemplatePath = $"EnviroSense.Plugins.RazorEmailing.EmailTemplates.{TemplateName}";

        _logger.LogDebug($"Rendering email template {fullTemplatePath}");

        var stopwatch = Stopwatch.StartNew();
        var result = await _razorLightEngine.CompileRenderAsync(fullTemplatePath, model);
        stopwatch.Stop();

        _logger.LogInformation($"Rendering {fullTemplatePath} took {stopwatch.ElapsedMilliseconds} ms");

        return new RenderedEmail
        {
            Title = Title,
            Body = result,
        };
    }
}
