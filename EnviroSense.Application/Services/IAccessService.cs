using EnviroSense.Domain.Entities;

namespace EnviroSense.Application.Services;

public interface IAccessService
{
    Task<Access> Create();
    Task<int> Count();
    Task<List<Access>> List();
}
