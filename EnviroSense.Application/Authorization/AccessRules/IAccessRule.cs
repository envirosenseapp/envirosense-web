namespace EnviroSense.Application.Authorization.AccessRules;

public interface IAccessRule<T>
{
    Task<bool> HasAccess(T entity);
}
