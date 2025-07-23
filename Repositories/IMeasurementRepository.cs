using System;
using EnviroSense.Web.Entities;

namespace EnviroSense.Web.Repositories;

public interface IMeasurementRepository
{
    Task<Measurement> CreateAsync(Measurement measurement);

}
