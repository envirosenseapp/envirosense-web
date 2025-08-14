
using EnviroSense.Domain.Entities;

namespace EnviroSense.Repositories.Repositories;

public interface IAccountPasswordResetRepository
{
    Task<AccountPasswordReset> CreateResetPasswordEntityAsync(AccountPasswordReset account);
    Task<AccountPasswordReset> FetchAccountPasswordResetEntityByIdAsync(Guid securityCode);

    Task<Account> UpdateAccountPasswordAsync(Guid accountId, string newPassword);

    Task<AccountPasswordReset> SetUsedAtTimeAsync(Guid accountId);

}
