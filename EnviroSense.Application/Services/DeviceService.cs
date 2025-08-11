using EnviroSense.Domain.Entities;
using EnviroSense.Domain.Exceptions;
using EnviroSense.Repositories.Repositories;

namespace EnviroSense.Application.Services;

public class DeviceService : IDeviceService
{
    private readonly IDeviceRepository _deviceRepository;
    private readonly IAccountService _accountService;

    public DeviceService(IDeviceRepository deviceRepository,
        IAccountService accountService)
    {
        _deviceRepository = deviceRepository;
        _accountService = accountService;
    }

    protected virtual Guid GetAccountId()
    {
        var accountId = _accountService.GetAccountIdFromSession();
        if (accountId == null)
        {
            throw new SessionIsNotAvailableException();
        }

        return Guid.Parse(accountId);
    }

    public Task<List<Device>> List()
    {
        var acountId = GetAccountId();
        return _deviceRepository.ListAsync(acountId);
    }

    public Task<Device?> Get(Guid Id)
    {
        return _deviceRepository.GetAsync(Id);
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

        return await _deviceRepository.CreateAsync(device);
    }
}
