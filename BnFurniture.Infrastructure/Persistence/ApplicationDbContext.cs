using BnFurniture.Domain.Constants;
using BnFurniture.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BnFurniture.Infrastructure.Persistence;

public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<AuditLog> AuditLog { get; set; }
    public DbSet<Characteristic> Characteristic { get; set; }
    public DbSet<CharacteristicValue> CharacteristicValue { get; set; }
    public DbSet<Order> Order { get; set; }
    public DbSet<OrderItem> OrderItem { get; set; }
    public DbSet<OrderStatus> OrderStatus { get; set; }
    public DbSet<PasswordResetToken> PasswordResetToken { get; set; }
    public DbSet<Permission> Permission { get; set; }
    public DbSet<Product> Product { get; set; }
    public DbSet<ProductArticle> ProductArticle { get; set; }
    public DbSet<ProductCategory> ProductCategory { get; set; }
    public DbSet<ProductCharacteristicConfiguration> ProductCharacteristicConfiguration { get; set; }
    public DbSet<ProductMetrics> ProductMetrics { get; set; }
    public DbSet<ProductReview> ProductReview { get; set; }
    public DbSet<ProductSet> ProductSet { get; set; }
    public DbSet<ProductSetCategory> ProductSetCategory { get; set; }
    public DbSet<ProductSetItem> ProductSetItem { get; set; }
    public DbSet<ProductType> ProductType { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<User_UserRole> User_UserRole { get; set; }
    public DbSet<UserActivityType> UserActivityType { get; set; }
    public DbSet<UserRole> UserRole { get; set; }
    public DbSet<UserRole_Permission> UserRole_Permission { get; set; }
    public DbSet<UserWishlist> UserWishlist { get; set; }
    public DbSet<UserWishlistItem> UserWishlistItem { get; set; }
    public DbSet<OrderItem> ProductArticleOrderItem { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ConfigureKeys(modelBuilder);
        ConfigureProperties(modelBuilder);
        ConfigureRelationship(modelBuilder);
        Seed(modelBuilder);
    }


    private void Seed(ModelBuilder modelBuilder)
    {
        var adminId = new Guid("ADAD13d5-2468-4f9c-9ddc-b0940569df8a");
        var adminRoleId = Guid.NewGuid();

        /* Permissions */
        var permissionList = new List<Permission>()
        {
            new() { Name = Permissions.DashboardAccess },
            new() { Name = Permissions.UpdateAccess },
            new() { Name = Permissions.DeleteAccess },
            new() { Name = Permissions.CreateAccess }
        };
        foreach (var permission in permissionList)
        {
            permission.Id = Guid.NewGuid();
            modelBuilder.Entity<Permission>().HasData(permission);
        }

        /* Admin User */
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = adminId,
                Email = "sashavannovski@gmail.com",
                Password = "8C6976E5B5410415BDE908BD4DEE15DFB167A9C873FC4BB8A81F6F2AB448A918",  // SHA256, "admin"
                FirstName = "Oleksandr",
                LastName = "Vannovskyi",
                RegisteredAt = DateTime.Now,
            });

        /* User Roles */
        modelBuilder.Entity<UserRole>().HasData(
            new UserRole { Id = adminRoleId, Name = "Admin" });

        /* UserRole_Permission */
        foreach (var permission in permissionList)
        {
            modelBuilder.Entity<UserRole_Permission>().HasData(
                new UserRole_Permission
                {
                    Id = Guid.NewGuid(),
                    UserRoleId = adminRoleId,
                    PermissionId = permission.Id
                });
        }

        /* User_UserRole */
        modelBuilder.Entity<User_UserRole>().HasData(
            new User_UserRole
            {
                Id = Guid.NewGuid(),
                UserId = adminId,
                UserRoleId = adminRoleId
            });
    }

    private void ConfigureProperties(ModelBuilder modelBuilder)
    {
        // Decimal precision - money
        modelBuilder.Entity<OrderItem>()
            .Property(e => e.Price)
            .HasPrecision(19, 2);
        modelBuilder.Entity<ProductArticle>()
            .Property(e => e.Price)
            .HasPrecision(19, 2);

        // 5-star rating
        modelBuilder.Entity<ProductReview>()
            .Property(e => e.Rating)
            .HasMaxLength(5);

        // Max discount value - 100
        modelBuilder.Entity<OrderItem>()
            .Property(e => e.Discount)
            .HasMaxLength(100);
        modelBuilder.Entity<ProductArticle>()
            .Property(e => e.Discount)
            .HasMaxLength(100);

        // Set `Unique` attribute to all `Slug` fields

        modelBuilder.Entity<ProductCategory>()
            .HasIndex(e => e.Slug)
            .IsUnique(true);
        /*
        modelBuilder.Entity<ProductType>()
            .HasIndex(e => e.Slug)
            .IsUnique(true);
        */
        modelBuilder.Entity<Characteristic>()
            .HasIndex(e => e.Slug)
            .IsUnique(true);
        /*
        modelBuilder.Entity<CharacteristicValue>()
            .HasIndex(e => e.Slug)
            .IsUnique(true);
        */
        modelBuilder.Entity<Product>()
            .HasIndex(e => e.Slug)
            .IsUnique(true);
        modelBuilder.Entity<ProductSet>()
            .HasIndex(e => e.Slug)
            .IsUnique(true); 
        modelBuilder.Entity<ProductSetCategory>()
            .HasIndex(e => e.Slug)
            .IsUnique(true);
    }

    private void ConfigureKeys(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductArticle_OrderItem>()
            .HasKey(a => new { a.OrderItemId, a.ProductArticleId });
    }

    private void ConfigureRelationship(ModelBuilder modelBuilder)
    {
        // Characteristic - * CharacteristicValue
        modelBuilder.Entity<Characteristic>()
            .HasMany(a => a.CharacteristicValues)
            .WithOne(b => b.Characteristic)
            .HasForeignKey(c => c.CharacteristicId)
            .OnDelete(DeleteBehavior.Restrict); // +

        // Characteristic - * ProductCharacteristicConfiguration
        modelBuilder.Entity<Characteristic>()
            .HasMany(a => a.ProductCharacteristicConfigurations)
            .WithOne(b => b.Characteristic)
            .HasForeignKey(c => c.CharacteristicId)
            .OnDelete(DeleteBehavior.Restrict); // +



        // CharacteristicValue - * ProductCharacteristicConfiguration
        modelBuilder.Entity<CharacteristicValue>()
            .HasMany(a => a.ProductCharacteristicConfigurations)
            .WithOne(b => b.CharacteristicValue)
            .HasForeignKey(c => c.CharacteristicValueId)
            .OnDelete(DeleteBehavior.Restrict); // +



        // Order - * OrderItem
        modelBuilder.Entity<Order>()
            .HasMany(a => a.OrderItems)
            .WithOne(b => b.Order)
            .HasForeignKey(c => c.OrderId)
            .OnDelete(DeleteBehavior.Cascade);  // +



        // OrderItem - * ProductArticle_OrderItem
        // (for ProductArticle * * OrderItem)
        // TODO: make sure this is correct behavior
        modelBuilder.Entity<OrderItem>()
            .HasMany(a => a.ProductArticles)
            .WithOne(b => b.OrderItem)
            .HasForeignKey(c => c.OrderItemId)
            .OnDelete(DeleteBehavior.ClientSetNull);    // ?



        // OrderStatus - * Order
        modelBuilder.Entity<OrderStatus>()
            .HasMany(a => a.Orders)
            .WithOne(b => b.Status)
            .HasForeignKey(c => c.StatusId)
            .OnDelete(DeleteBehavior.Restrict); // +



        // Permission - * UserRole_Permission
        modelBuilder.Entity<Permission>()
            .HasMany(a => a.UserRole_Permissions)
            .WithOne(b => b.Permission)
            .HasForeignKey(c => c.PermissionId)
            .OnDelete(DeleteBehavior.Restrict); // +



        // User - - User_UserRole
        modelBuilder.Entity<User>()
            .HasOne(a => a.User_UserRole)
            .WithOne(b => b.User)
            .HasForeignKey<User_UserRole>(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);  // +

        // User - * Order
        modelBuilder.Entity<User>()
            .HasMany(a => a.Orders)
            .WithOne(b => b.User)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);  // +

        // User - * ProductSet
        modelBuilder.Entity<User>()
            .HasMany(a => a.ProductSets)
            .WithOne(b => b.Author)
            .HasForeignKey(c => c.AuthorId)
            .OnDelete(DeleteBehavior.Restrict); // +

        // User - * Product
        modelBuilder.Entity<User>()
            .HasMany(a => a.Products)
            .WithOne(b => b.Author)
            .HasForeignKey(c => c.AuthorId)
            .OnDelete(DeleteBehavior.Restrict); // +

        // User - * ProductReview
        modelBuilder.Entity<User>()
            .HasMany(a => a.ProductReviews)
            .WithOne(b => b.Author)
            .HasForeignKey(c => c.AuthorId)
            .OnDelete(DeleteBehavior.Cascade);  // +

        // User - * ProductArticle
        modelBuilder.Entity<User>()
            .HasMany(a => a.ProductArticles)
            .WithOne(b => b.Author)
            .HasForeignKey(c => c.AuthorId)
            .OnDelete(DeleteBehavior.Restrict); // +

        // User - * UserWishlist
        modelBuilder.Entity<User>()
            .HasMany(a => a.UserWishlists)
            .WithOne(b => b.User)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);  // +

        // User - * AuditLog
        modelBuilder.Entity<User>()
            .HasMany(a => a.AuditLogs)
            .WithOne(b => b.User)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);  // ?

        // User - * PasswordResetToken
        modelBuilder.Entity<User>()
            .HasMany(a => a.PasswordResetTokens)
            .WithOne(b => b.User)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);  // +



        // Product - - ProductMetrics
        modelBuilder.Entity<Product>()
            .HasOne(a => a.Metrics)
            .WithOne(b => b.Product)
            .HasForeignKey<ProductMetrics>(c => c.ProductId)
            .OnDelete(DeleteBehavior.Cascade);  // +

        // Product - * ProductSetItem
        // TODO: make sure this is correct behavior
        modelBuilder.Entity<Product>()
            .HasMany(a => a.ProductSetItems)
            .WithOne(b => b.Product)
            .HasForeignKey(c => c.ProductId)
            .OnDelete(DeleteBehavior.Cascade);  // ?

        // Product - * ProductReview
        modelBuilder.Entity<Product>()
            .HasMany(a => a.ProductReviews)
            .WithOne(b => b.Product)
            .HasForeignKey(c => c.ProductId)
            .OnDelete(DeleteBehavior.Cascade);  // +

        // Product - * ProductArticle
        modelBuilder.Entity<Product>()
            .HasMany(a => a.ProductArticles)
            .WithOne(b => b.Product)
            .HasForeignKey(c => c.ProductId)
            .OnDelete(DeleteBehavior.Cascade);  // +



        // ProductArticle - * ProductArticle_OrderItem
        // (for ProductArticle * * OrderItem)
        // TODO: make sure this is correct behavior
        modelBuilder.Entity<ProductArticle>()
            .HasMany(a => a.OrderItems)
            .WithOne(b => b.ProductArticle)
            .HasForeignKey(c => c.ProductArticleId)
            .OnDelete(DeleteBehavior.Cascade);  // ?

        // ProductArticle - * ProductCharacteristicConfiguration
        modelBuilder.Entity<ProductArticle>()
            .HasMany(a => a.ProductCharacteristicConfigurations)
            .WithOne(b => b.ProductArticle)
            .HasForeignKey(c => c.ArticleId)
            .OnDelete(DeleteBehavior.Cascade);  // +

        // ProductArticle - * UserWishlistItem
        modelBuilder.Entity<ProductArticle>()
            .HasMany(a => a.UserWishlistItems)
            .WithOne(b => b.ProductArticle)
            .HasForeignKey(c => c.ProductArticleId)
            .OnDelete(DeleteBehavior.Cascade);  // +



        // ProductCategory - - ProductCategory (Parent Id)
        modelBuilder.Entity<ProductCategory>()
            .HasOne(a => a.ParentCategory)
            .WithMany()
            .HasForeignKey(c => c.ParentId)
            .OnDelete(DeleteBehavior.Restrict); // +

        // ProductCategory - * ProductType
        modelBuilder.Entity<ProductCategory>()
            .HasMany(a => a.ProductTypes)
            .WithOne(b => b.ProductCategory)
            .HasForeignKey(c => c.CategoryId)
            .OnDelete(DeleteBehavior.Restrict); // +



        // ProductSet - * ProductSetItem
        modelBuilder.Entity<ProductSet>()
            .HasMany(a => a.ProductSetItems)
            .WithOne(b => b.ProductSet)
            .HasForeignKey(c => c.ProductSetId)
            .OnDelete(DeleteBehavior.Cascade);  // +



        // ProductSetCategory - - ProductSetCategory (Parent Id)
        modelBuilder.Entity<ProductSetCategory>()
            .HasOne(a => a.ParentCategory)
            .WithMany()
            .HasForeignKey(c => c.ParentId)
            .OnDelete(DeleteBehavior.Restrict); // +

        // ProductSetCategory - * ProductSet
        modelBuilder.Entity<ProductSetCategory>()
            .HasMany(a => a.ProductSets)
            .WithOne(b => b.ProductSetCategory)
            .HasForeignKey(c => c.SetCategoryId)
            .OnDelete(DeleteBehavior.Restrict); // +



        // ProductType - * Product
        modelBuilder.Entity<ProductType>()
            .HasMany(a => a.Products)
            .WithOne(b => b.ProductType)
            .HasForeignKey(c => c.ProductTypeId)
            .OnDelete(DeleteBehavior.Restrict); // +



        // UserActivityType - * AuditLog
        modelBuilder.Entity<UserActivityType>()
            .HasMany(a => a.AuditLogs)
            .WithOne(b => b.UserActivityType)
            .HasForeignKey(c => c.UserActivityTypeId)
            .OnDelete(DeleteBehavior.Restrict); // +



        // UserRole - * User_UserRole
        modelBuilder.Entity<UserRole>()
            .HasMany(a => a.User_UserRoles)
            .WithOne(b => b.UserRole)
            .HasForeignKey(c => c.UserRoleId)
            .OnDelete(DeleteBehavior.Restrict); // +

        // UserRole - * UserRole_Permission
        modelBuilder.Entity<UserRole>()
            .HasMany(a => a.UserRole_Permissions)
            .WithOne(b => b.UserRole)
            .HasForeignKey(c => c.UserRoleId)
            .OnDelete(DeleteBehavior.Restrict); // +



        // UserWishlist - * UserWishlistItem
        modelBuilder.Entity<UserWishlist>()
            .HasMany(a => a.UserWishlistItems)
            .WithOne(b => b.UserWishlist)
            .HasForeignKey(c => c.UserWishlistId)
            .OnDelete(DeleteBehavior.Cascade);  // +

      
    }
}