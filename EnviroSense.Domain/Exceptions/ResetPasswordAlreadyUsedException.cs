
namespace EnviroSense.Domain.Exceptions;

public class ResetPasswordAlreadyUsedException : Exception
{
    public ResetPasswordAlreadyUsedException() : base("Reset password already used") { }

}
