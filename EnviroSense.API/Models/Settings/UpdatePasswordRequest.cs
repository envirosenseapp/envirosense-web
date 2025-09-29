using System.ComponentModel.DataAnnotations;

namespace EnviroSense.API.Models.Settings;

public class UpdatePasswordRequest
{
    [Required]
    [MinLength(8)]
    public String? NewPassword { get; set; }

    [Required]
    [Compare("NewPassword")]
    [MinLength(8)]
    public string? ConfirmPassword { get; set; }
}
