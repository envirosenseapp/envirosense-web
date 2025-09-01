using System.ComponentModel.DataAnnotations;

namespace EnviroSense.Web.ViewModels.ApiKeys;

public class CreateApiKeyViewModel
{
    [MinLength(6)]
    [MaxLength(120)]
    [Required]
    public required string Name { get; set; }
}
