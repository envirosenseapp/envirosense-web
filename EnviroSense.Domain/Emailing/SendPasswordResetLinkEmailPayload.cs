using System.Runtime.InteropServices.JavaScript;

namespace EnviroSense.Domain.Emailing;

public class SendPasswordResetLinkEmailPayload : BaseEmail
{
    public required DateTime LoginDate { get; set; }
    public required string PasswordResetEmail { get; set; }
    public required string PasswordResetLink { get; set; }
}
