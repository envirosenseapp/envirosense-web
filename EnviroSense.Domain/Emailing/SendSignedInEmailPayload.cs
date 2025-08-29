using System.Runtime.InteropServices.JavaScript;

namespace EnviroSense.Domain.Emailing;

public class SendSignedInEmailPayload : BaseEmail
{
    public required DateTime LoginDate { get; set; }
}
