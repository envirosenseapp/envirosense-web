using System;

namespace EnviroSense.Web.Entities;

public class Device
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<Measurement>? Measurements { get; set; }

}
