using System.ComponentModel.DataAnnotations;

namespace EnviroSense.Web.Entities;

public class Access
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    [StringLength(45)] public required string IpAddress { get; set; }
    [StringLength(512)] public required string Client { get; set; }
    [StringLength(1024)] public required string Resource { get; set; }
}
