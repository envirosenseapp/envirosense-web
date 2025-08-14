namespace EnviroSense.Domain.Exceptions;

public class NotAuthenticatedException : Exception
{
    public NotAuthenticatedException(string msg) : base(msg)
    {

    }
}
