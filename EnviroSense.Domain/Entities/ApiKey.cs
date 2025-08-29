namespace EnviroSense.Domain.Entities;

public class ApiKey
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string KeyHash { get; set; }
    
    public Guid AccountId { get; set; }
    public virtual required Account Account { get; set; }
    public DateTime? DisabledAt { get; set; }
    public DateTime CreatedAt { get; set; }
}
