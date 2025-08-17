namespace EnviroSense.Domain.Exceptions;

public class AccountPasswordResetNotFoundException : Exception
{
    public AccountPasswordResetNotFoundException() : base("Account password reset not found") { }
}
