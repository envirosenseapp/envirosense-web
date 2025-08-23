namespace EnviroSense.API.Models.Core;

public class InternalServiceError : BaseError
{
    public InternalServiceError() : base(
        "internal_server_error",
        "unexpected error occured"
        )
    {
    }
}
