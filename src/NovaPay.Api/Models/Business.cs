namespace NovaPay.Api.Models;

public enum KycStatus
{
    Pending,
    Verified,
    Rejected
}

public class Business
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string? Country { get; set; }
    public KycStatus KycStatus { get; set; } = KycStatus.Pending;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    // Navigation
    public ICollection<User> Users { get; set; } = new List<User>();
    public ICollection<Account> Accounts { get; set; } = new List<Account>();
    public Subscription? Subscription { get; set; }
    public ICollection<SecurityEvent> SecurityEvents { get; set; } = new List<SecurityEvent>();
}
