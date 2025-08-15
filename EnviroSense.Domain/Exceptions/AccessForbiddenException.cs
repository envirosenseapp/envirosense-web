namespace EnviroSense.Domain.Exceptions;

public class AccessForbiddenException : Exception
{
    public AccessForbiddenException() : base("Access forbidden")
    {

    }
}
