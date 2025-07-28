namespace EnviroSense.Web.Exceptions;

public class DeviceNotFoundException : Exception
{
    public DeviceNotFoundException(Guid deviceId) : base($"device with ID {deviceId} not found") { }
}
