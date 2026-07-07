namespace NovaPay.Api.Models;

public enum TransactionDirection
{
    Inbound,
    Outbound
}

public enum TransactionStatus
{
    Pending,
    Processing,
    Completed,
    Failed
}

public class Transaction
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid AccountId { get; set; }
    public Account Account { get; set; } = null!;

    public TransactionDirection Direction { get; set; }
    public decimal Amount { get; set; }

    public string CurrencyCode { get; set; } = string.Empty;
    public Currency Currency { get; set; } = null!;

    public string? CounterpartyName { get; set; }
    public string? CounterpartyCountry { get; set; }

    public TransactionStatus Status { get; set; } = TransactionStatus.Pending;
    public DateTimeOffset? SettledAt { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}

