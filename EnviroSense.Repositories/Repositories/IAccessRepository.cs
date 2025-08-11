using EnviroSense.Domain.Entities;

namespace EnviroSense.Repositories.Repositories;

public interface IAccessRepository
{
    Task<Access> CreateAsync(Access access);
    Task<int> Count();
    Task<List<Access>> ListAsync();
}
