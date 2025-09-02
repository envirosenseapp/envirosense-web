using System.Runtime.InteropServices.JavaScript;

namespace EnviroSense.Domain.Emailing;

public class SendPasswordResetNotificationEmailPayload : BaseEmail
{
    public required DateTime ResetAt { get; set; }
    public required string PasswordResetEmail { get; set; }
    public required string RedirectLink { get; set; }
}
