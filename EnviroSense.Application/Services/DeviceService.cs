using EnviroSense.Domain.Entities;
using EnviroSense.Domain.Exceptions;
using EnviroSense.Repositories.Repositories;

namespace EnviroSense.Application.Services;

public class DeviceService : IDeviceService
{
    private readonly IDeciveRepository _deciveRepository;
    private readonly IAccountService _accountService;

    public DeviceService(IDeciveRepository deciveRepository,
        IAccountService accountService)
    {
        _deciveRepository = deciveRepository;
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
