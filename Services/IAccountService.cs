using EnviroSense.Web.Entities;

namespace EnviroSense.Web.Services;

public interface IAccountService
{
    Task<bool> IsEmailTaken(string email);
    Task<Account> Add(Account account);
}
