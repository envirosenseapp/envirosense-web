namespace EnviroSense.API.Models.Core;

public class ForbiddenError : BaseError
{
    public ForbiddenError() : base(
        "forbidden",
        "access to this resources is forbidden"
    )
    {
    }
}
