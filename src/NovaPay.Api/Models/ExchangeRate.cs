namespace NovaPay.Api.Models;

public class ExchangeRate
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string BaseCurrencyCode { get; set; } = string.Empty;
    public Currency BaseCurrency { get; set; } = null!;

    public string QuoteCurrencyCode { get; set; } = string.Empty;
    public Currency QuoteCurrency { get; set; } = null!;

    public decimal Rate { get; set; }

    // Nullable: only set when a business has locked this rate via FX hedging
    public Guid? LockedByBusinessId { get; set; }
    public Business? LockedByBusiness { get; set; }
    public DateTimeOffset? LockedUntil { get; set; }

    public DateTimeOffset RecordedAt { get; set; } = DateTimeOffset.UtcNow;
}
