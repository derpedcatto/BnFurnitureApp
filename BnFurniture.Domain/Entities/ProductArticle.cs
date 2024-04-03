
namespace ASP_Work.Data.Entity
{
    public class ProductArticle
    {
        public Guid Article { get; set; }
        public Guid Product_Id { get; set; }
        public Guid Author_id { get; set; }
        public String Name { get; set; } = null!;
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public decimal Price { get; set; }
        public int Discount { get; set; }
        public Boolean Active { get; set; }

        // Navigation property for the related ProductCharacteristicConfiguration
        public ICollection<ProductCharacteristicConfiguration> ProductCharacteristicConfiguration { get; set; } = null!;

        // Navigation property for the related Product
        public Product? Product { get; set; }= null!;

        // Navigation property for the related Product
        public User? User_Pa { get; set; } = null!;

        // Navigation property for the related UserWishlistItem
        public ICollection<UserWishlistItem> UserWishlistItems_Pa { get; set; } = null!;
    }
}
