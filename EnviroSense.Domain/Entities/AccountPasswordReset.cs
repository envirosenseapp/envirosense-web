namespace EnviroSense.Domain.Entities;

public class AccountPasswordReset
{
    public Guid Id { get; set; }
    public required Account Account { get; set; }
    public required Guid AccountId { get; set; }

    public required Guid SecurityCode { get; set; }
    public DateTime? UsedAt { get; set; }
    public required DateTime ResetDate { get; set; }
}
