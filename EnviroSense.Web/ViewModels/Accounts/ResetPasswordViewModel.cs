using System.ComponentModel.DataAnnotations;

namespace EnviroSense.Web.ViewModels.Accounts;

public class ResetPasswordViewModel
{
    [Required]
    [DataType(DataType.Password)]
    [MinLength(8)]
    public String? NewPassword { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Compare("NewPassword")]
    [MinLength(8)]
    public string? ConfirmPassword { get; set; }
}
