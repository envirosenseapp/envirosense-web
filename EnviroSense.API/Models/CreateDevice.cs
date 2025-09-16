using System.ComponentModel.DataAnnotations;

namespace EnviroSense.API.Models;

public class CreateDevice
{
    [MaxLength(50)]
    public required string Name { get; set; }
}
