using EnviroSense.Application.Authentication;
using EnviroSense.Application.Authorization;
using EnviroSense.Domain.Entities;
using EnviroSense.Domain.Exceptions;
using EnviroSense.Repositories.Repositories;

namespace EnviroSense.Application.Services;

public class DeviceService : IDeviceService
{
    private readonly IDeviceRepository _deviceRepository;
    private readonly IAuthorizationResolver _authorizationResolver;
    private readonly IAuthenticationRetriever _authenticationRetriever;

    public DeviceService(
        IDeviceRepository deviceRepository,
        IAuthorizationResolver authorizationResolver,
        IAuthenticationRetriever authenticationRetriever
    )
    {
        _deviceRepository = deviceRepository;
        _authorizationResolver = authorizationResolver;
        _authenticationRetriever = authenticationRetriever;
    }

    public async Task<List<Device>> List()
    {
        var accountId = await _authenticationRetriever.GetCurrentAccountId();
        if (accountId == null)
        {
            throw new SessionIsNotAvailableException();
        }

        return await _deviceRepository.ListAsync(accountId.Value);
    }

    public async Task<Device?> Get(Guid id)
    {
        var device = await _deviceRepository.GetAsync(id);
        await _authorizationResolver.MustHaveAccess(device);

        return device;
    }

    public async Task<Device> Create(string name)
    {
        var account = await _authenticationRetriever.GetCurrentAccount();
        if (account == null)
        {
            throw new SessionIsNotAvailableException();
        }

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
