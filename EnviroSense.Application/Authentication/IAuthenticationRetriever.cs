using EnviroSense.Domain.Entities;

namespace EnviroSense.Application.Authentication;

public interface IAuthenticationRetriever
{
    Guid? GetCurrentAccountId();
}
