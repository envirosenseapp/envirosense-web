using EnviroSense.Web.Repositories;

namespace EnviroSense.Web.Services;

public class DeviceService : IDeviceService
{
    private readonly IDeciveRepository _deciveRepository;
    public DeviceService(IDeciveRepository deciveRepository)
    {
        _deciveRepository = deciveRepository;
    }

}
