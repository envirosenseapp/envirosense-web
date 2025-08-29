using EnviroSense.Domain.Emailing;

namespace EnviroSense.Application.Emailing;

public interface IEmailRenderer<T>
{
    Task<RenderedEmail> Render(T model);
}
