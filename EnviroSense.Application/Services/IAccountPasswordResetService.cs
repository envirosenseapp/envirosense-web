
using EnviroSense.Domain.Entities;

namespace EnviroSense.Application.Services;

public interface IAccountPasswordResetService
{
    Task<Guid?> ResetPasswordAsync(string email);

    Task<Account> Reset(Guid securityCode, string newPassword);

    Task<Account> ResetPasswordFromSettings(string email, string password);

}
