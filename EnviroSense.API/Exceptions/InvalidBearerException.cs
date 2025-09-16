namespace EnviroSense.API.Exceptions;

public class InvalidBearerException : Exception
{
    public InvalidBearerException(string additionalReason) : base($"Invalid Bearer exception ({additionalReason})")
    {

    }
}
