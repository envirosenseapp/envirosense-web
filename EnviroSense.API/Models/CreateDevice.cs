using System.ComponentModel.DataAnnotations;

namespace EnviroSense.API.Models;

public class CreateDevice
{
    [Required(ErrorMessage = "Name is required")]
    [MaxLength(50)]
    public required string Name { get; set; }
}
