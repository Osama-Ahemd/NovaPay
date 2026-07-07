namespace NovaPay.Api.Models;

public class PricingPlan
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty; // "Starter" | "Growth" | "Enterprise"
    public decimal MonthlyPrice { get; set; }
    public decimal AnnualPrice { get; set; }
    public int MaxTeamMembers { get; set; }
    public int MaxCurrencies { get; set; }
    public decimal? MonthlyVolumeCap { get; set; } // null = unlimited

    public ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
}
