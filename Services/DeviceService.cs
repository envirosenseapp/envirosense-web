using EnviroSense.Web.Entities;
using EnviroSense.Web.Exceptions;
using EnviroSense.Web.Repositories;

namespace EnviroSense.Web.Services;

public class DeviceService : IDeviceService
{
    private readonly IDeciveRepository _deciveRepository;
    private readonly IHttpContextAccessor _accessor;
    private readonly IAccountService _accountService;

    public DeviceService(IDeciveRepository deciveRepository, IHttpContextAccessor accessor,
        IAccountService accountService)
    {
        _deciveRepository = deciveRepository;
        _accessor = accessor;
        _accountService = accountService;
    }

    private Guid GetAccountId()
    {
        var httpContext = _accessor.HttpContext;

        if (httpContext == null || httpContext.Session == null)
        {
            throw new SessionIsNotAvailableException();
        }

        var session = httpContext.Session;
        var accountId = session.GetString("authenticated_account_id");
        if (accountId == null)
        {
            throw new SessionIsNotAvailableException();
        }

        return Guid.Parse(accountId);
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

    public async Task<Device> Create(string name)
    {
        var accountId = GetAccountId();
        var account = await _accountService.GetAccountById(accountId);
        var device = new Device
        {
            Name = name,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Measurements = new List<Measurement>(),
            Account = account,
        };

        return await _deciveRepository.CreateAsync(device);
    }
}
