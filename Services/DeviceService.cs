using EnviroSense.Web.Entities;
using EnviroSense.Web.Exceptions;
using EnviroSense.Web.Repositories;

namespace EnviroSense.Web.Services;

public class DeviceService : IDeviceService
{
    private readonly IDeciveRepository _deciveRepository;
    private readonly IHttpContextAccessor _accessor;

    public DeviceService(IDeciveRepository deciveRepository, IHttpContextAccessor accessor)
    {
        _deciveRepository = deciveRepository;
        _accessor = accessor;
    }

    public Guid GetAccountId()
    {
        var httpContext = _accessor.HttpContext;

        if (httpContext == null || httpContext.Session == null)
        {
            throw new SessionIsNotAvailableException();
        }

        var session = httpContext.Session;
        var accountIdStr = session.GetString("authenticated_account_id");
        if (accountIdStr == null)
        {
            throw new SessionIsNotAvailableException();
        }

        return Guid.Parse(accountIdStr);
    }

    public Task<List<Device>> List()
    {
        var acountId = GetAccountId();
        return _deciveRepository.ListAsync(acountId);
    }

    public Task<Device?> Get(Guid Id)
    {
        return _deciveRepository.GetAsync(Id);
    }

    public Task<Device> Create(string name)
    {
        var device = new Device
        {
            Name = name,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            AccountId = GetAccountId(),
            Measurements = new List<Measurement>()
        };

        return _deciveRepository.CreateAsync(device);
    }
}
