namespace EnviroSense.API.Models.Core;

public class PagedResult<T>
{
    public ICollection<T> Records { get; set; }

    public PagedResult(IEnumerable<T> data)
    {
        Records = data.ToList();
    }
}
