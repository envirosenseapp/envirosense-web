using System.ComponentModel.DataAnnotations;

namespace EnviroSense.API.Models.Auth;

public class SignInRequest
{
    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    [Required]
    [MinLength(8)]
    public string? Password { get; set; }
}
