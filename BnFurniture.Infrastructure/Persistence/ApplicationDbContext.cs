using BnFurniture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace BnFurniture.Infrastructure.Persistence
{
    public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
    {
        // public DbSet<ExampleEntity> ExampleEntity { get; set; }
        public DbSet<AuditLog> AuditLog { get; set; }
        public DbSet<Characteristics> Characteristics { get; set; }
        public DbSet<CharacteristicsValue> CharacteristicsValue { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<OrderStatus> OrderStatus { get; set; }
        public DbSet<PasswordResetToken> PasswordResetToken { get; set; }
        public DbSet<Permission> Permission { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductArticle> ProductArticle { get; set; }
        public DbSet<ProductArticleOrderItem> ProductArticleOrderItem { get; set; }
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
        public DbSet<ProductArticleOrderItem> ProductArticleOrderItems { get; set; }


        public async ValueTask<int> SaveChangesAsync(CancellationToken cancellationToken = default, [CallerMemberName] string? callerFunction = null, [CallerFilePath] string? callerFile = null) =>
            await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Указание строки подключения для MySQL
                optionsBuilder.UseMySql("DefaultConnection", new MySqlServerVersion(new Version(8, 0, 30)));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuditLog>()
                .HasKey(pt => pt.Id); // Id в качестве первичного ключа для таблицы AuditLog

            modelBuilder.Entity<Characteristics>()
               .HasKey(pt => pt.Id); // Id в качестве первичного ключа для таблицы Characteristics

            modelBuilder.Entity<CharacteristicsValue>()
                .HasKey(pt => pt.Id); // Id в качестве первичного ключа для таблицы CharacteristicsValue


            modelBuilder.Entity<Order>()
               .HasKey(pt => pt.Id); // Id в качестве первичного ключа для таблицы Order

            modelBuilder.Entity<OrderItem>()
              .HasKey(pt => pt.Id); // Id в качестве первичного ключа для таблицы OrderItem

            modelBuilder.Entity<OrderStatus>()
             .HasKey(pt => pt.Id); // Id в качестве первичного ключа для таблицы OrderStatus

            modelBuilder.Entity<PasswordResetToken>()
             .HasKey(pt => pt.Id); // Id в качестве первичного ключа для таблицы PasswordResetToken

            modelBuilder.Entity<Permission>()
            .HasKey(pt => pt.Id); // Id в качестве первичного ключа для таблицы Permission


            modelBuilder.Entity<Product>()
                .HasKey(p => p.Id); // Id в качестве первичного ключа для таблицы Product

            modelBuilder.Entity<ProductArticle>()
               .HasKey(p => p.Article); // Article в качестве первичного ключа для таблицы ProductArticle



            modelBuilder.Entity<ProductCategory>()
               .HasKey(pt => pt.Id); // Id в качестве первичного ключа для таблицы ProductCategory

            //modelBuilder.Entity<ProductCategoryTop>() TABLE DELETED
            //   .HasKey(pt => pt.Id); // Id в качестве первичного ключа для таблицы ProductCategoryTop

            modelBuilder.Entity<ProductCharacteristicConfiguration>()
                .HasKey(pt => pt.Id); // Id в качестве первичного ключа для таблицы ProductCharacteristicConfiguration


            modelBuilder.Entity<ProductMetrics>()
               .HasKey(pt => pt.Id); // Id в качестве первичного ключа для таблицы ProductMetrics


            modelBuilder.Entity<ProductReview>()
               .HasKey(pt => pt.Id); // Id в качестве первичного ключа для таблицы ProductReview


            modelBuilder.Entity<ProductSet>()
              .HasKey(pt => pt.Id); // Id в качестве первичного ключа для таблицы ProductSet

            modelBuilder.Entity<ProductSetCategory>()
              .HasKey(pt => pt.Id); // Id в качестве первичного ключа для таблицы ProductSetCategory

            modelBuilder.Entity<ProductSetItem>()
              .HasKey(pt => pt.Id); // Id в качестве первичного ключа для таблицы ProductSetItem

            modelBuilder.Entity<ProductType>()
                .HasKey(pt => pt.Id); // Id в качестве первичного ключа для таблицы ProductType

            modelBuilder.Entity<User>()
                .HasKey(pt => pt.Id); // Id в качестве первичного ключа для таблицы User
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique(); // Добавляем уникальное ограничение для поля Email
            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100)  // Необходимая длина
                .HasAnnotation("RegularExpression", @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"); // Регулярное выражение для формата электронной почты

            modelBuilder.Entity<User_UserRole>()
              .HasKey(pt => pt.Id); // Id в качестве первичного ключа для таблицы User_UserRole

            modelBuilder.Entity<UserActivityType>()
              .HasKey(pt => pt.Id); // Id в качестве первичного ключа для таблицы UserActivityType

            modelBuilder.Entity<UserRole>()
             .HasKey(pt => pt.Id); // Id в качестве первичного ключа для таблицы UserRole

            modelBuilder.Entity<UserRole_Permission>()
             .HasKey(pt => pt.Id); // Id в качестве первичного ключа для таблицы UserRole_Permission

            modelBuilder.Entity<UserWishlist>()
            .HasKey(pt => pt.Id); // Id в качестве первичного ключа для таблицы UserWishlist

            modelBuilder.Entity<UserWishlistItem>()
            .HasKey(pt => pt.Id); // Id в качестве первичного ключа для таблицы UserWishlistItem


            //Устанавливаем связь между таблицами Product & ProductType
            modelBuilder.Entity<Product>()
                .HasOne(p => p.ProductType)
                .WithMany(pt => pt.Products)
                .HasForeignKey(p => p.Productype_Id)
                .OnDelete(DeleteBehavior.Restrict);

            //Устанавливаем связь между таблицами ProductType & ProductCategory
            modelBuilder.Entity<ProductType>()
            .HasOne(pt => pt.ProductCategory)
            .WithMany(pc => pc.ProductType)
            .HasForeignKey(pt => pt.Category_id)
            .OnDelete(DeleteBehavior.Restrict);

            //Устанавливаем связь между таблицами ProductArticle & ProductCharacteristicConfiguration
            modelBuilder.Entity<ProductCharacteristicConfiguration>()
            .HasOne(pcc => pcc.ProductArticles)
            .WithMany(pa => pa.ProductCharacteristicConfiguration)
            .HasForeignKey(pcc => pcc.Article_Id)
            .OnDelete(DeleteBehavior.Restrict);

            //Устанавливаем связь между таблицами Product & ProductArticle
            modelBuilder.Entity<ProductArticle>()
            .HasOne(pcc => pcc.Product)
            .WithMany(pa => pa.ProductArticles)
            .HasForeignKey(pcc => pcc.Product_Id)
            .OnDelete(DeleteBehavior.Restrict);

            //Устанавливаем связь между таблицами ProductSet & ProductSetCategory
            modelBuilder.Entity<ProductSet>()
            .HasOne(pcc => pcc.ProductSetCategory)
            .WithMany(pa => pa.ProductSets)
            .HasForeignKey(pcc => pcc.Setcategory_Id)
            .OnDelete(DeleteBehavior.Restrict);

            //Устанавливаем связь между таблицами User & Order
            modelBuilder.Entity<Order>()
            .HasOne(pcc => pcc.User)
            .WithMany(pa => pa.Orders)
            .HasForeignKey(pcc => pcc.User_Id)
            .OnDelete(DeleteBehavior.Restrict);

            //Устанавливаем связь между таблицами User & ProductSet
            modelBuilder.Entity<ProductSet>()
            .HasOne(pcc => pcc.User_Ps)
            .WithMany(pa => pa.ProductSets)
            .HasForeignKey(pcc => pcc.Author_Id)
            .OnDelete(DeleteBehavior.Restrict);

            //Устанавливаем связь между таблицами User & ProductReview
            modelBuilder.Entity<ProductReview>()
            .HasOne(pcc => pcc.User_Pr)
            .WithMany(pa => pa.ProductReviews)
            .HasForeignKey(pcc => pcc.User_Id)
            .OnDelete(DeleteBehavior.Restrict);

            //Устанавливаем связь между таблицами User & Product
            modelBuilder.Entity<Product>()
            .HasOne(pcc => pcc.User_Pr)
            .WithMany(pa => pa.Products_Us)
            .HasForeignKey(pcc => pcc.Productype_Id)
            .OnDelete(DeleteBehavior.Restrict);

            //Устанавливаем связь между таблицами User & ProductArticle
            modelBuilder.Entity<ProductArticle>()
            .HasOne(pcc => pcc.User_Pa)
            .WithMany(pa => pa.ProductArticles_Us)
            .HasForeignKey(pcc => pcc.Author_id)
            .OnDelete(DeleteBehavior.Restrict);

            //Устанавливаем связь между таблицами User & UserWishlist
            modelBuilder.Entity<UserWishlist>()
            .HasOne(pcc => pcc.User_Wl)
            .WithMany(pa => pa.UserWishlist_Us)
            .HasForeignKey(pcc => pcc.User_Id)
            .OnDelete(DeleteBehavior.Restrict);

            //Устанавливаем связь между таблицами ProductArticle & UserWishlistItem
            modelBuilder.Entity<UserWishlistItem>()
            .HasOne(pcc => pcc.ProductArticle_Uwl)
            .WithMany(pa => pa.UserWishlistItems_Pa)
            .HasForeignKey(pcc => pcc.Article_Id)
            .OnDelete(DeleteBehavior.Restrict);

            //Устанавливаем связь между таблицами Characteristics & CharacteristicsValue
            modelBuilder.Entity<CharacteristicsValue>()
            .HasOne(cv => cv.Characteristics)
            .WithMany(pc => pc.CharacteristicsValues)
            .HasForeignKey(pcc => pcc.Сharacteristic_id)
            .OnDelete(DeleteBehavior.Restrict);

            //Устанавливаем связь между таблицами Product & ProductSetItem
            modelBuilder.Entity<ProductSetItem>()
            .HasOne(cv => cv.Products)
            .WithMany(pc => pc.ProductSetItem)
            .HasForeignKey(pcc => pcc.Product_Id)
            .OnDelete(DeleteBehavior.Restrict);

            //Устанавливаем связь между таблицами Product & ProductReview
            modelBuilder.Entity<ProductReview>()
            .HasOne(pv => pv.Products)
            .WithMany(pb => pb.ProductReview)
            .HasForeignKey(pcb => pcb.Product_id)
            .OnDelete(DeleteBehavior.Restrict);

            ////Устанавливаем связь между таблицами Order & OrderItem
            modelBuilder.Entity<OrderItem>()
            .HasOne(pz => pz.Order)
            .WithMany(pb => pb.OrderItems)
            .HasForeignKey(pca => pca.Order_Id)
            .OnDelete(DeleteBehavior.Restrict);

            //Устанавливаем связь между таблицами Order & OrderStatus
            modelBuilder.Entity<Order>()
            .HasOne(po => po.Status)
            .WithMany(pb => pb.Orders)
            .HasForeignKey(pca => pca.Status_Id)
            .OnDelete(DeleteBehavior.Restrict);

            //Устанавливаем связь между таблицами ProductMetrics & Product
            modelBuilder.Entity<Product>()
               .HasOne(p => p.Metrics)
               .WithOne(pm => pm.Product)
               .HasForeignKey<ProductMetrics>(pm => pm.Product_Id);

            //Устанавливаем связь между таблицами UserWishlistItem & UserWishlist
            modelBuilder.Entity<UserWishlistItem>()
            .HasOne(po => po.UserWishlist)
            .WithMany(pb => pb.UserWishlistItems)
            .HasForeignKey(pca => pca.Wishlist_Id)
            .OnDelete(DeleteBehavior.Restrict);

            //Устанавливаем связь между таблицами ProductSet & ProductSetItem
            modelBuilder.Entity<ProductSetItem>()
            .HasOne(po => po.ProductSet)
            .WithMany(pb => pb.ProductSetItems)
            .HasForeignKey(pca => pca.ProductSet_Id)
            .OnDelete(DeleteBehavior.Restrict);

            //Устанавливаем связь между таблицами ProductArticle & OrderItem многие ко многим
            modelBuilder.Entity<ProductArticleOrderItem>()
                .HasKey(pa => new { pa.ProductArticleId, pa.OrderItemId });

            modelBuilder.Entity<ProductArticleOrderItem>()
                .HasOne(pa => pa.ProductArticle)
                .WithMany()
                .HasForeignKey(pa => pa.ProductArticleId);

            modelBuilder.Entity<ProductArticleOrderItem>()
                .HasOne(pa => pa.OrderItem)
                .WithMany()
                .HasForeignKey(pa => pa.OrderItemId);

            //Устанавливаем связь между таблицами User & AuditLog
            modelBuilder.Entity<AuditLog>()
            .HasOne(cv => cv.User_AuLog)
            .WithMany(pc => pc.AuditLogs)
            .HasForeignKey(pcc => pcc.User_Id)
            .OnDelete(DeleteBehavior.Restrict);

            //Устанавливаем связь между таблицами UserActivityType & AuditLog
            modelBuilder.Entity<AuditLog>()
            .HasOne(cv => cv.UserActivityType_AuLog)
            .WithMany(pc => pc.AuditLogs_UsAcT)
            .HasForeignKey(pcc => pcc.Useractivitytype_Id)
            .OnDelete(DeleteBehavior.Restrict);


            //Устанавливаем связь между таблицами Permission & UserRole_Permissions
            modelBuilder.Entity<UserRole_Permission>()
            .HasOne(cv => cv.Permission)
            .WithMany(pc => pc.UserRole_Permissions)
            .HasForeignKey(pcc => pcc.Permission_Id)
            .OnDelete(DeleteBehavior.Restrict);

            //Устанавливаем связь между таблицами UserRole & UserRole_Permissions
            modelBuilder.Entity<UserRole_Permission>()
            .HasOne(cv => cv.UserRole)
            .WithMany(pc => pc.UserRole_Permissions_UsRol)
            .HasForeignKey(pcc => pcc.Role_Id)
            .OnDelete(DeleteBehavior.Restrict);

            //Устанавливаем связь между таблицами UserRole & User_UserRole
            modelBuilder.Entity<User_UserRole>()
            .HasOne(cv => cv.UserRole)
            .WithMany(pc => pc.User_UserRoles)
            .HasForeignKey(pcc => pcc.UserRole_Id)
            .OnDelete(DeleteBehavior.Restrict);

            //Устанавливаем связь между таблицами User & User_UserRole
            modelBuilder.Entity<User_UserRole>()
            .HasOne(cv => cv.User_Us_UsRole)
            .WithMany(pc => pc.User_UserRoles_Us)
            .HasForeignKey(pcc => pcc.User_Id)
            .OnDelete(DeleteBehavior.Restrict);

            //Устанавливаем связь между таблицами User & PasswordResetToken
            modelBuilder.Entity<PasswordResetToken>()
            .HasOne(cv => cv.User_PasswordResetToken)
            .WithMany(pc => pc.PasswordResetTokens)
            .HasForeignKey(pcc => pcc.User_Id)
            .OnDelete(DeleteBehavior.Restrict);



            base.OnModelCreating(modelBuilder);
        }
    }
}
