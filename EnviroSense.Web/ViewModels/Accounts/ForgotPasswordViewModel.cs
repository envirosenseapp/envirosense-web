
using System.ComponentModel.DataAnnotations;

namespace EnviroSense.Web.ViewModels.Accounts;

public class ForgotPasswordViewModel
{
    [Required(ErrorMessage = "Please enter a email address")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address")]
    public required string Email { get; set; }
}
