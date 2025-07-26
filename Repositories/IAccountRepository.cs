using EnviroSense.Web.Entities;

namespace EnviroSense.Web.Repositories;

public interface IAccountRepository
{
    Task<bool> IsEmailTaken(string email);
    Task<Account> AddAsync(Account account);
}
