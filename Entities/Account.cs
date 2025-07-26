namespace EnviroSense.Web.Entities;

using System.ComponentModel.DataAnnotations;

public class Account
{
    public Guid Id { get; set; }
    [StringLength(320)] public required string Email { get; set; }
    [StringLength(128)] public required string Password { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }
}
