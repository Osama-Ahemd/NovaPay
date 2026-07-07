using Microsoft.EntityFrameworkCore;
using NovaPay.Api.Models;

namespace NovaPay.Api
{

    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Business> Businesses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<ExchangeRate> ExchangeRates { get; set; }
        public DbSet<PricingPlan> PricingPlans { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<SecurityEvent> SecurityEvents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ---- Currency (string PK) ----
            modelBuilder.Entity<Currency>(e =>
            {
                e.HasKey(c => c.Code);
                e.Property(c => c.Code).HasMaxLength(3);
            });

            // ---- Business -> Users (1:N) ----
            modelBuilder.Entity<User>()
                .HasOne(u => u.Business)
                .WithMany(b => b.Users)
                .HasForeignKey(u => u.BusinessId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // ---- Business -> Accounts (1:N), Account -> Currency (N:1) ----
            modelBuilder.Entity<Account>()
                .HasOne(a => a.Business)
                .WithMany(b => b.Accounts)
                .HasForeignKey(a => a.BusinessId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Account>()
                .HasOne(a => a.Currency)
                .WithMany()
                .HasForeignKey(a => a.CurrencyCode)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Account>()
                .Property(a => a.Balance)
                .HasColumnType("decimal(18,2)");

            // ---- Account -> Transactions (1:N), Transaction -> Currency (N:1) ----
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Account)
                .WithMany(a => a.Transactions)
                .HasForeignKey(t => t.AccountId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Currency)
                .WithMany()
                .HasForeignKey(t => t.CurrencyCode)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transaction>()
                .Property(t => t.Amount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Transaction>()
                .HasIndex(t => new { t.AccountId, t.CreatedAt }); // dashboard / ticker queries

            // ---- ExchangeRate: two FKs to Currency (base + quote) ----
            modelBuilder.Entity<ExchangeRate>()
                .HasOne(r => r.BaseCurrency)
                .WithMany()
                .HasForeignKey(r => r.BaseCurrencyCode)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ExchangeRate>()
                .HasOne(r => r.QuoteCurrency)
                .WithMany()
                .HasForeignKey(r => r.QuoteCurrencyCode)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ExchangeRate>()
                .HasOne(r => r.LockedByBusiness)
                .WithMany()
                .HasForeignKey(r => r.LockedByBusinessId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<ExchangeRate>()
                .Property(r => r.Rate)
                .HasColumnType("decimal(18,8)");

            // ---- Business <-> Subscription (1:1) ----
            modelBuilder.Entity<Subscription>()
                .HasOne(s => s.Business)
                .WithOne(b => b.Subscription)
                .HasForeignKey<Subscription>(s => s.BusinessId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Subscription>()
                .HasIndex(s => s.BusinessId)
                .IsUnique(); // enforces the 1:1

            modelBuilder.Entity<Subscription>()
                .HasOne(s => s.Plan)
                .WithMany(p => p.Subscriptions)
                .HasForeignKey(s => s.PlanId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PricingPlan>(e =>
            {
                e.Property(p => p.MonthlyPrice).HasColumnType("decimal(10,2)");
                e.Property(p => p.AnnualPrice).HasColumnType("decimal(10,2)");
                e.Property(p => p.MonthlyVolumeCap).HasColumnType("decimal(18,2)");
            });

            // ---- Business -> SecurityEvents (1:N), optional link to Transaction ----
            modelBuilder.Entity<SecurityEvent>()
                .HasOne(s => s.Business)
                .WithMany(b => b.SecurityEvents)
                .HasForeignKey(s => s.BusinessId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SecurityEvent>()
                .HasOne(s => s.Transaction)
                .WithMany()
                .HasForeignKey(s => s.TransactionId)
                .OnDelete(DeleteBehavior.SetNull);

            // Store enums as readable strings instead of ints — much easier to read
            // directly in the DB when debugging (e.g. "Completed" instead of "2").
            modelBuilder.Entity<Business>().Property(b => b.KycStatus).HasConversion<string>();
            modelBuilder.Entity<User>().Property(u => u.Role).HasConversion<string>();
            modelBuilder.Entity<Transaction>().Property(t => t.Direction).HasConversion<string>();
            modelBuilder.Entity<Transaction>().Property(t => t.Status).HasConversion<string>();
            modelBuilder.Entity<Subscription>().Property(s => s.BillingCycle).HasConversion<string>();
            modelBuilder.Entity<Subscription>().Property(s => s.Status).HasConversion<string>();
            modelBuilder.Entity<SecurityEvent>().Property(s => s.EventType).HasConversion<string>();
            modelBuilder.Entity<SecurityEvent>().Property(s => s.Severity).HasConversion<string>();
        }
    }

}
