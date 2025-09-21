using System.ComponentModel.DataAnnotations;

namespace EnviroSense.API.Models.RessetPassword;

public class ForgotPasswordRequest
{
    [Required(ErrorMessage = "Please enter a email address")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address")]
    public required string Email { get; set; }
}
