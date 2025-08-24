using EnviroSense.Domain.Entities;

namespace EnviroSense.Application.Authentication;

public interface IAuthenticationRetriever
{
    Task<Guid?> GetCurrentAccountId();
    Task<Account?> GetCurrentAccount();
}
