using System;
using EnviroSense.Web.Entities;

namespace EnviroSense.Web.Services;

public interface IMeasurementService
{
    Task<Measurement> Create(DateTime recordingDate, string temperature, string humidity, Guid deviceId);
    Task<List<Measurement>> List(Guid deviceId);
}
