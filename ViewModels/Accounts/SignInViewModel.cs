using System.ComponentModel.DataAnnotations;

namespace EnviroSense.Web.ViewModels.Accounts;

public class SignInViewModel
{
    [Required]
    [EmailAddress]
    [MaxLength(320)]
    public required string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [MinLength(8)]
    public required string Password { get; set; }
}
