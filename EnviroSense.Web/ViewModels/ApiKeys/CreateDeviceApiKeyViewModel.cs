using System.ComponentModel.DataAnnotations;

namespace EnviroSense.Web.ViewModels.ApiKeys;

public class CreateDeviceApiKeyViewModel
{
    [MinLength(6)]
    [MaxLength(120)]
    [Required]
    public string? Name { get; set; }
}
