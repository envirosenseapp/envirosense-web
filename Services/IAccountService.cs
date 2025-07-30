using EnviroSense.Web.Entities;

namespace EnviroSense.Web.Services;

public interface IAccountService
{
    Task<bool> IsEmailTaken(string email);
    Task<Account> Add(string email, string password);
    Task<Account> Login(string email, string password);
    Task<Account> GetAccountById(Guid accountId);
}
