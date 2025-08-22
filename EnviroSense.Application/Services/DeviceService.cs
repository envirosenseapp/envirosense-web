using EnviroSense.Application.Authentication;
using EnviroSense.Application.Authorization;
using EnviroSense.Domain.Entities;
using EnviroSense.Domain.Exceptions;
using EnviroSense.Repositories.Repositories;

namespace EnviroSense.Application.Services;

public class DeviceService : IDeviceService
{
    private readonly IDeviceRepository _deviceRepository;
    private readonly IAccountService _accountService;
    private readonly IAuthorizationResolver _authorizationResolver;
    private readonly IAuthenticationRetriever _authenticationRetriever;

    public DeviceService(
        IDeviceRepository deviceRepository,
        IAccountService accountService,
        IAuthorizationResolver authorizationResolver,
        IAuthenticationRetriever authenticationRetriever
    )
    {
        _deviceRepository = deviceRepository;
        _accountService = accountService;
        _authorizationResolver = authorizationResolver;
        _authenticationRetriever = authenticationRetriever;
    }

    protected virtual Guid GetAccountId()
    {
        var accountId = _authenticationRetriever.GetCurrentAccountId();
        if (accountId == null)
        {
            throw new SessionIsNotAvailableException();
        }

        return accountId.Value;
    }

    public Task<List<Device>> List()
    {
        var acountId = GetAccountId();
        return _deviceRepository.ListAsync(acountId);
    }

    public async Task<Device?> Get(Guid id)
    {
        var device = await _deviceRepository.GetAsync(id);
        await _authorizationResolver.MustHaveAccess(device);

        return device;
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
            Account = account,
        };

        return await _deviceRepository.CreateAsync(device);
    }
}
