namespace BnFurniture.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public String Email { get; set; } = null!;
        public String? Phonenumber { get; set; }
        public String Password { get; set; } = null!;
        public String FirstName { get; set; } = null!;
        public String LastName { get; set; } = null!;
        public String? Address { get; set; }
        public DateTime Created { get; set; }
        public DateTime? LastLogin_At { get; set; } = null;

        // Navigation property for the related Order
        public ICollection<Order> Orders { get; set; }= null!;
        // Navigation property for the related ProductSet ProductReview
        public ICollection<ProductSet> ProductSets { get; set; } = null!;

        // Navigation property for the related  ProductReview
        public ICollection<ProductReview> ProductReviews { get; set; } = null!;

        // Navigation property for the related  Product
        public ICollection<Product> Products_Us { get; set; } = null!;

        // Navigation property for the related  ProductArticle 
        public ICollection<ProductArticle> ProductArticles_Us { get; set; } = null!;

        // Navigation property for the related  UserWishlist
        public ICollection<UserWishlist> UserWishlist_Us { get; set; } = null!;

        // Navigation property for the related  UserWishlist
        public ICollection<AuditLog> AuditLogs { get; set; } = null!;

        // Navigation property for the related  User_UserRole
        public ICollection<User_UserRole> User_UserRoles_Us { get; set; } = null!;

        // Navigation property for the related  User_UserRole
        public ICollection<PasswordResetToken> PasswordResetTokens { get; set; } = null!;
    }
}
