using EnviroSense.Domain.Entities;

namespace EnviroSense.Repositories.Repositories;

public interface IAccountPasswordResetRepository
{
    Task<AccountPasswordReset> CreateAsync(AccountPasswordReset account);
    Task<AccountPasswordReset> GetBySecurityCodeAsync(Guid securityCode);

    Task<Account> UpdateAccountPasswordAsync(Guid accountId, string newPassword);
}
