
namespace EnviroSense.Application.Services;

public interface IAccountPasswordResetService
{
    Task<bool> ResetPasswordAsync(string email);

}
