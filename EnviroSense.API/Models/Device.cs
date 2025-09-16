namespace EnviroSense.API.Models;

public class Device
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }
}
