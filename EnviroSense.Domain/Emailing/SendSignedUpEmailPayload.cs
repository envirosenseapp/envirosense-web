namespace EnviroSense.Domain.Emailing;

public class SendSignedUpEmailPayload : BaseEmail
{
    public required DateTime LoginDate { get; set; }
}
