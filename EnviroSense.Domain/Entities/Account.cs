namespace EnviroSense.Domain.Entities;

public class Account
{
    public Guid Id { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Device> Devices { get; set; } = new List<Device>();
    public virtual ICollection<Access> Accesses { get; set; } = new List<Access>();
    public virtual ICollection<AccountPasswordReset> PasswordResets { get; set; } = new List<AccountPasswordReset>();
}
