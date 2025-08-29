namespace EnviroSense.Domain.Emailing;

public abstract class BaseEmail
{
    public required string Email { get; set; }
    public required string Title { get; set; }
}
