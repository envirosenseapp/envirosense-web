using EnviroSense.Web.Entities;

namespace EnviroSense.Web.Repositories;

public interface IAccountRepository
{
    Task<bool> ValidateAsync(string email);
    Task<Account> AddAsync(Account account);
}
