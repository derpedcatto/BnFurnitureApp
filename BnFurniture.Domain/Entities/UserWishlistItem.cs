namespace BnFurniture.Domain.Entities
{
    public class UserWishlistItem
    {
        public Guid Id { get; set; }
        public Guid Article_Id { get; set; }
        public Guid Wishlist_Id { get; set; }
        public DateTime? Updated { get; set; }

        // Navigation property for the related UserWishlist
        public UserWishlist? UserWishlist { get; set; } = null!;

        // Navigation property for the related ProductArticle
        public ProductArticle? ProductArticle_Uwl { get; set; } = null!;
    }
}
