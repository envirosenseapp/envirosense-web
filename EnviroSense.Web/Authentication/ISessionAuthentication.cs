using EnviroSense.Application.Authentication;
using EnviroSense.Domain.Entities;

namespace EnviroSense.Web.Authentication;

public interface ISessionAuthentication : IAuthenticationContext
{
    Task<Account> Login(string email, string password);

    void Logout();
}
