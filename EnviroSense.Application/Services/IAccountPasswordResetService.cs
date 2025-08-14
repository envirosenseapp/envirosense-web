
using EnviroSense.Domain.Entities;

namespace EnviroSense.Application.Services;

public interface IAccountPasswordResetService
{
    Task<(bool IsAccountToReset, Guid SecurityCode)> ResetPasswordAsync(string email);

    Task<AccountPasswordReset> FetchAccountPasswordResetEntityById(Guid securityCode, string newPassword);

}
