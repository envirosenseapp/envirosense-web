using EnviroSense.Domain.Entities;

namespace EnviroSense.Repositories.Repositories;

public interface IAccountRepository
{
    Task<bool> IsEmailTaken(string email);
    Task<Account> AddAsync(Account account);
    Task<Account> GetAccountByEmailAsync(string email);
    Task<Account> GetAccountByIdAsync(Guid accountId);
}
