using System.ComponentModel.DataAnnotations;

namespace EnviroSense.Web.ViewModels.DeviceApiKeys;

public class CreateDeviceApiKeyViewModel
{
    # region Output fields
    public string? DeviceName { get; set; }
    public Guid DeviceId { get; set; }
    #endregion

    #region Input fields


    [MinLength(6)]
    [MaxLength(120)]
    [Required]
    public string? Name { get; set; }

    #endregion

}
