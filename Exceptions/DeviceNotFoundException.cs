namespace EnviroSense.Web.Exceptions;

public class DeviceNotFoundException : Exception
{
    public DeviceNotFoundException(Guid DeviceId) : base($"device with ID {DeviceId} not found") { }
}
