namespace NovaPay.Api.Models;

public class Account
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid BusinessId { get; set; }
    public Business Business { get; set; } = null!;

    public string CurrencyCode { get; set; } = string.Empty;
    public Currency Currency { get; set; } = null!;

    public decimal Balance { get; set; } = 0m;
    public bool IsPrimary { get; set; } = false;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
