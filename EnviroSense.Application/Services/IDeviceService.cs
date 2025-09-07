using EnviroSense.Domain.Entities;
using EnviroSense.Domain.Filters;
using EnviroSense.Repositories.Core;

namespace EnviroSense.Application.Services;

public interface IDeviceService
{
    Task<PagedList<Device>> List(int PageIndex);
    Task<Device?> Get(Guid id);
    Task<Device> Create(string name);
}
