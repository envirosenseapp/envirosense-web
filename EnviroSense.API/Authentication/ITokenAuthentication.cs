using EnviroSense.Domain.Entities;

namespace EnviroSense.API.Authentication;

public interface ITokenAuthentication
{
    Task<string> Login(string email, string password);
}
