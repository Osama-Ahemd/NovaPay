namespace NovaPay.Api.Models;

public enum UserRole
{
    Owner,
    Admin,
    Member
}

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid BusinessId { get; set; }
    public Business Business { get; set; } = null!;

    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public UserRole Role { get; set; } = UserRole.Member;
    public bool TwoFactorEnabled { get; set; } = false;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}
