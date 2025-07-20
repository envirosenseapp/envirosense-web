using EnviroSense.Web.Entities;
using EnviroSense.Web.Repositories;

namespace EnviroSense.Web.Services;

public class AccessService: IAccessService
{
    private readonly IAccessRepository _accessRepository;

    public AccessService(IAccessRepository accessRepository)
    {
        _accessRepository = accessRepository;
    }

    public async Task<Access> Create()
    {
        var access = new Access
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
        };

        var createdAccess = await _accessRepository.CreateAsync(access);

        return createdAccess;
    }

    public Task<int> Count()
    {
        return _accessRepository.Count();
    }
}