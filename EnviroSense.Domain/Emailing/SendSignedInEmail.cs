using System.Runtime.InteropServices.JavaScript;

namespace EnviroSense.Domain.Emailing;

public class SendSignedInEmail : BaseEmail
{
    public required DateTime LoginDate { get; set; }
}
