using EnviroSense.Web.Entities;

namespace EnviroSense.Web.Services;

public interface IMeasurementService
{
    Task<Measurement> Create(DateTime recordingDate, float temperature, float humidity, Guid deviceId);
    Task<List<Measurement>?> List(Guid deviceId);
}
