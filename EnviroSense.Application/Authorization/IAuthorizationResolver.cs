namespace EnviroSense.Application.Authorization;

public interface IAuthorizationResolver
{
    Task<bool> HasAccess<T>(T entity);
    Task MustHaveAccess<T>(T entity);
}
