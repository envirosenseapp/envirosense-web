namespace EnviroSense.Web;

public class EmailSetings
{
    public required string Host { get; set; }
    public required int Port { get; set; }
    public bool UseSsl { get; set; }
    public required string From { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
}
