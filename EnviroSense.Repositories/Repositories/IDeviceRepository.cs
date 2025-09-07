using EnviroSense.Domain.Entities;
using EnviroSense.Domain.Filters;
using EnviroSense.Repositories.Core;

namespace EnviroSense.Repositories.Repositories;

public interface IDeviceRepository
{
    Task<PagedList<Device>> ListAsync(DeviceFiltres filters);
    Task<Device> GetAsync(Guid id);
    Task<Device> CreateAsync(Device device);
}
