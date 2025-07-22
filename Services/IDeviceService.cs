using System;
using EnviroSense.Web.Entities;

namespace EnviroSense.Web.Services;

public interface IDeviceService
{
    Task<List<Device>> List();
    Task<Device> Get(Guid Id);
    Task<Device> Create(string name);
}
