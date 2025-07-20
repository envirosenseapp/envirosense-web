namespace EnviroSense.Web.Entities;

public class Access
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string IpAddress { get; set; }
    public string Client { get; set; }
    public string Resource { get; set; }
}