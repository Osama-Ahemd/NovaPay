namespace NovaPay.Api.Models;


public enum SecurityEventType
{
    FraudFlag,
    Login,
    TwoFactorChallenge,
    Anomaly
}

public enum SecurityEventSeverity
{
    Info,
    Warning,
    Blocked
}


// Append-only audit log — never update or delete rows, only insert.
public class SecurityEvent
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid BusinessId { get; set; }
    public Business Business { get; set; } = null!;

    public Guid? TransactionId { get; set; }
    public Transaction? Transaction { get; set; }

    public SecurityEventType EventType { get; set; }
    public SecurityEventSeverity Severity { get; set; } = SecurityEventSeverity.Info;
    public string? Detail { get; set; }

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}
