namespace EnviroSense.API.Models.Core;

public class UnauthenticatedError: BaseError
{
    public UnauthenticatedError(params BaseError.Entry[] entries) : base(
        "unauthenticated",
        "missing or invalid authentication",
        entries
    )
    {
    }
}
