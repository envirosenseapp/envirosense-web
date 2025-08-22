using EnviroSense.Domain.Entities;

namespace EnviroSense.Application.Services;

public interface IAccountService
{
    Task<bool> IsEmailTaken(string email);
    Task<Account> Add(string email, string password);
    Task<Account> GetAccountById(Guid accountId);

    Task<Account> GetAccountByEmail(string email);
    Task<Account> ResetPasswordFromSettings(Guid id, string password);
}
