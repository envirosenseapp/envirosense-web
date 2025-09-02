using System.Runtime.InteropServices.JavaScript;

namespace EnviroSense.Domain.Emailing;

public class SendPasswordResetedNotificationEmailPayload : BaseEmail
{
    public required DateTime LoginDate { get; set; }
    public required string PasswordResetEmail { get; set; }
    public required string RedirectLink { get; set; }
}
