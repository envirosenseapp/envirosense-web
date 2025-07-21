using EnviroSense.Web.Entities;

namespace EnviroSense.Web.Repositories;
public interface IAccessRepository
{
    Task<Access> CreateAsync(Access access);
    Task<int> Count();
    Task<List<Access>> ListAsync();
}