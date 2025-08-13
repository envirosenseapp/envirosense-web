
using EnviroSense.Domain.Entities;

namespace EnviroSense.Repositories.Repositories;

public interface IAccountPasswordResetRepository
{
    Task<AccountPasswordReset> CreateResetPasswordEntityAsync(AccountPasswordReset account);

}
