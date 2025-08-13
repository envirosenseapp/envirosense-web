// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.ComponentModel.DataAnnotations;

namespace EnviroSense.Web.ViewModels.DeviceApiKeys;

public class DetailsDeviceApiKeyViewModel
{
    public string? DeviceName { get; set; }
    public Guid DeviceId { get; set; }

    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? RevealedKey { get; set; }
    public DateTime? DisabledAt { get; set; }
    public DateTime CreatedAt { get; set; }
}
