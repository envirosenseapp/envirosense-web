using EnviroSense.Web.Entities;

namespace EnviroSense.Web.Repositories;

public interface IDeciveRepository
{
    Task<List<Device>> ListAsync();
}
