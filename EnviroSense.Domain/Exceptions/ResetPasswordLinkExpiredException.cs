namespace EnviroSense.Domain.Exceptions;

public class ResetPasswordLinkExpiredException : Exception
{
    public ResetPasswordLinkExpiredException() : base("Reeset password link expired") { }
}
