using System.Runtime.InteropServices.JavaScript;

namespace EnviroSense.Domain.Emailing;

public class SendPasswordChangedNotificationEmailPayload : BaseEmail
{
    public required DateTime LoginDate { get; set; }
    public required string PasswordResetEmail { get; set; }
}
