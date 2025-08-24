namespace EnviroSense.API.Exceptions;

public class UnauthenticatedException: Exception
{
    public UnauthenticatedException(): base("Unauthenticated.")
    {
        
    }
}
