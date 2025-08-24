using EnviroSense.Domain.Entities;

namespace EnviroSense.Application.Authentication;

public interface IAuthenticationContext
{
    Task<Guid?> CurrentAccountId();
    Task<Account?> CurrentAccount();
}
