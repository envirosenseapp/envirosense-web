// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

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
