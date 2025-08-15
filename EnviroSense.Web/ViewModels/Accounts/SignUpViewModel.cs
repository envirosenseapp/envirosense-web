using System.ComponentModel.DataAnnotations;

namespace EnviroSense.Web.ViewModels.Accounts;

public class SignUpViewModel
{
    [Required]
    [EmailAddress]
    [MaxLength(320)]
    public required string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [MinLength(8)]
    public required String Password { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Compare("Password")]
    [MinLength(8)]
    public required string ConfirmPassword { get; set; }
}
