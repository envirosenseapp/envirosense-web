namespace EnviroSense.Application.Emailing;

public interface IEmailRenderer<T>
{
    Task<string> Render(T model);
}
