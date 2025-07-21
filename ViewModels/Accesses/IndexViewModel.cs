namespace EnviroSense.Web.ViewModels.Accesses;

public class AccessesViewModel
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string IpAddress { get; set; }
    public string Client { get; set; }
    public string Resource { get; set; }

}
