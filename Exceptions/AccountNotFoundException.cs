namespace EnviroSense.Web.Exceptions;

public class AccountNotFoundException : Exception
{
    public AccountNotFoundException() : base("Incorect email or password") { }
}
