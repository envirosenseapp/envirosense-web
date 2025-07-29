// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

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
