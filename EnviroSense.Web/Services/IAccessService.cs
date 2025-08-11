using EnviroSense.Domain.Entities;
using EnviroSense.Web.Entities;

namespace EnviroSense.Web.Services;

public interface IAccessService
{
    Task<Access> Create();
    Task<int> Count();
    Task<List<Access>> List();
}
