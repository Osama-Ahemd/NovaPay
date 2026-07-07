namespace NovaPay.Api.Models;

public enum BillingCycle
{
    Monthly,
    Annual
}

public enum SubscriptionStatus
{
    Active,
    Trialing,
    Canceled
}

public class Subscription
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid BusinessId { get; set; } // unique -> 1:1 with Business
    public Business Business { get; set; } = null!;

    public Guid PlanId { get; set; }
    public PricingPlan Plan { get; set; } = null!;

    public BillingCycle BillingCycle { get; set; } = BillingCycle.Monthly;
    public SubscriptionStatus Status { get; set; } = SubscriptionStatus.Trialing;
    public DateTimeOffset CurrentPeriodEnd { get; set; }
}
