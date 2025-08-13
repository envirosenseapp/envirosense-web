namespace EnviroSense.Domain.Entities;

public class Account
{
    public Guid Id { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public required ICollection<Device> Devices { get; set; }
    public ICollection<Access>? Accesses { get; set; }

    public ICollection<AccountPasswordReset> Resets { get; set; }
}
