namespace EnviroSense.Domain.Entities;

public class Device
{
    public Guid Id { get; set; }
    public Guid AccountId { get; set; }
    public virtual required Account Account { get; set; }
    public required string Name { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Measurement> Measurements { get; set; } = new List<Measurement>();

}
