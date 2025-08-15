using EnviroSense.Application.Authorization.AccessRules;
using EnviroSense.Application.Services;
using EnviroSense.Domain.Exceptions;
using EnviroSense.Repositories.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace EnviroSense.Application.Authorization;

public class AuthorizationResolver : IAuthorizationResolver
{
    private readonly IServiceProvider _serviceProvider;

    public AuthorizationResolver(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task<bool> HasAccess<T>(T entity)
    {
        var accessRule = _serviceProvider.GetRequiredService<IAccessRule<T>>();

        return accessRule.HasAccess(entity);
    }

    public async Task MustHaveAccess<T>(T entity)
    {
        var hasAccess = await HasAccess(entity);
        if (!hasAccess)
        {
            throw new AccessToForbiddenEntityException($@"Access to {entity.GetType().Name} is forbidden.");
        }
    }
}
