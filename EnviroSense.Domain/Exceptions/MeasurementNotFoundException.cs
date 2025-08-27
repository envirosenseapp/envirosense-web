namespace EnviroSense.Domain.Exceptions;

public class MeasurementNotFoundException : Exception
{
    public MeasurementNotFoundException(Guid deviceId) : base($"measurement with ID {deviceId} not found") { }
}

