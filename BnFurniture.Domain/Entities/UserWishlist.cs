namespace BnFurniture.Domain.Entities
{
    public class UserWishlist
    {
        public Guid Id { get; set; }
        public Guid User_Id { get; set; }

        // Navigation property for the related UserWishlistItems
        public ICollection<UserWishlistItem>? UserWishlistItems { get; set; } = null!;

        // Navigation property for the related User
        public User? User_Wl{ get; set; }=null!;
    }
}
