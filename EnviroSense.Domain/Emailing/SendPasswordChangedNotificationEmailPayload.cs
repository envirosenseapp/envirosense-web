using System.Runtime.InteropServices.JavaScript;

namespace EnviroSense.Domain.Emailing;

public class SendPasswordChangedNotificationEmailPayload : BaseEmail
{
    public required DateTime ChangedAt { get; set; }
    public required string PasswordResetEmail { get; set; }
}
