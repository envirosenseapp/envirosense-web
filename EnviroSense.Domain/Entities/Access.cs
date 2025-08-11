namespace EnviroSense.Domain.Entities;

public class Access
{
    public Guid Id { get; set; }
    public Guid? AccountId { get; set; }
    public Account? Account { get; set; }
    public DateTime CreatedAt { get; set; }
    public required string IpAddress { get; set; }
    public required string Client { get; set; }
    public required string Resource { get; set; }
}
