namespace EnviroSense.Domain.Entities;

public class DeviceApiKey
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public required string KeyHash { get; set; }

    public Guid DeviceId { get; set; }

    public virtual required Device Device { get; set; }

    public DateTime? DisabledAt { get; set; }

    public DateTime CreatedAt { get; set; }
}
