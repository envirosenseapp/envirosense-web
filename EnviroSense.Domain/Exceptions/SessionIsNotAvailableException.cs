namespace EnviroSense.Domain.Exceptions;

public class SessionIsNotAvailableException : Exception
{
    public SessionIsNotAvailableException() : base("Session is not available")
    {
    }
}
