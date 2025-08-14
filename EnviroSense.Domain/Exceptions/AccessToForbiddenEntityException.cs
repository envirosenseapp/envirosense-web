namespace EnviroSense.Domain.Exceptions;

public class AccessToForbiddenEntityException : Exception
{
    public AccessToForbiddenEntityException(string msg) : base(msg)
    {

    }
}
