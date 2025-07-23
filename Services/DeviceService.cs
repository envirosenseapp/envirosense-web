using EnviroSense.Web.Entities;
using EnviroSense.Web.Repositories;

namespace EnviroSense.Web.Services;

public class DeviceService : IDeviceService
{
    private readonly IDeciveRepository _deciveRepository;
    public DeviceService(IDeciveRepository deciveRepository)
    {
        _deciveRepository = deciveRepository;
    }
    public Task<List<Device>> List()
    {
        return _deciveRepository.ListAsync();
    }
    public Task<Device> Get(Guid Id)
    {
        return _deciveRepository.GetAsync(Id);
    }
    public Task<Device> Create(string name)
    {
        var device = new Device
        {
            Name = name,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        return _deciveRepository.CreateAsync(device);
    }
}
