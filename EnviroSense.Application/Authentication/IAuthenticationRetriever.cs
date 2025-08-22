using EnviroSense.Domain.Entities;

namespace EnviroSense.Application.Authentication;

public interface IAuthenticationRetriever
{
    Task<Account> Login(string email, string password);

    Guid? GetAccountIdFromSession();

    void Logout();
}
