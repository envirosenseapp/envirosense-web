namespace EnviroSense.Domain.Exceptions;

public class IpAddressNotFoundException : Exception
{
    public IpAddressNotFoundException() : base("ip address not found") { }

}
