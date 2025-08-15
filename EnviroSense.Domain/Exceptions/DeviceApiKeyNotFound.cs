namespace EnviroSense.Domain.Exceptions;

public class DeviceApiKeyNotFound : Exception
{
    public DeviceApiKeyNotFound(string msg) : base(msg)
    {

    }
}
