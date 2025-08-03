// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace EnviroSense.Web;

public class EmailSetings
{
    public required string Host { get; set; }
    public int Port { get; set; }
    public bool UseSsl { get; set; }
    public required string From { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
}
