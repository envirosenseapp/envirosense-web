using EnviroSense.Domain.Entities;

namespace EnviroSense.Application.Services;

public interface IDeviceService
{
    Task<List<Device>> List();
    Task<Device?> Get(Guid id);
    Task<Device> Create(string name);
}
