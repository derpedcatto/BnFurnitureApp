using System.ComponentModel.DataAnnotations;

namespace BnFurniture.Domain.Entities;

public class User
{
    [Key]
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public string Password { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Address { get; set; }
    public DateTime RegisteredAt { get; set; }
    public DateTime? LastLoginAt { get; set; }

    // Nav
    public ICollection<Order> Orders { get; set; } = null!;
    public ICollection<ProductSet> ProductSets { get; set; } = null!;
    public ICollection<Product> Products { get; set; } = null!;
    public ICollection<ProductReview> ProductReviews { get; set; } = null!;
    public ICollection<ProductArticle> ProductArticles { get; set; } = null!;
    public ICollection<UserWishlist> UserWishlists { get; set; } = null!;
    public ICollection<AuditLog> AuditLogs { get; set; } = null!;
    public ICollection<PasswordResetToken> PasswordResetTokens { get; set; } = null!;
    public User_UserRole User_UserRole { get; set; } = null!;
}
