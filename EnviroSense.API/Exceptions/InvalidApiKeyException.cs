namespace EnviroSense.API.Exceptions;

public class InvalidApiKeyException : Exception
{
    public InvalidApiKeyException(string additionalReason) : base($"Invalid API Key exception ({additionalReason})")
    {

    }
}
