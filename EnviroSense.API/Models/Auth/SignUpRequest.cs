using System.ComponentModel.DataAnnotations;

namespace EnviroSense.API.Models.Auth;

public class SignUpRequest
{
    [Required]
    [EmailAddress]
    [MaxLength(320)]
    public required string Email { get; set; }

    [Required]
    [MinLength(8)]
    public required String Password { get; set; }

    [Required]
    [Compare("Password")]
    [MinLength(8)]
    public required string ConfirmPassword { get; set; }
}
