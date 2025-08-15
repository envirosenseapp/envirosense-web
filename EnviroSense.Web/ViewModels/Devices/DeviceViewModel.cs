namespace EnviroSense.Web.ViewModels.Devices;

public class DeviceViewModel
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }

    public IEnumerable<ApiKeyDetails> ApiKeys { get; set; } = new List<ApiKeyDetails>();

    public class ApiKeyDetails
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public DateTime? DisabledAt { get; set; }
    }
}
