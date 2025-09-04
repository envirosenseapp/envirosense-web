namespace EnviroSense.Domain.Exceptions;

public class MeasurementNotFoundException : Exception
{
    public MeasurementNotFoundException(Guid deviceId) : base($"measurements for ID {deviceId} not found") { }
}

