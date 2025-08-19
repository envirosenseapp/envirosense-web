using System.ComponentModel.DataAnnotations;

namespace EnviroSense.Web.ViewModels.Accounts;

public class SettingsViewModel
{
    public DateTime UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string? Email { get; set; }

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
