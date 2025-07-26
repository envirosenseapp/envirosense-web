using System.ComponentModel.DataAnnotations;

namespace EnviroSense.Web.Entities;

public class Device
{
    public Guid Id { get; set; }
    [StringLength((30))] public required string Name { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public required ICollection<Measurement> Measurements { get; set; }
}
