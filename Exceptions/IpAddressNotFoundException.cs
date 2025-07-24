using System;

namespace EnviroSense.Web.Exceptions;

public class IpAddressNotFoundException : Exception
{
    public IpAddressNotFoundException() : base("ip address not found") { }

}
