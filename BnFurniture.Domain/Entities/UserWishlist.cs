using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BnFurniture.Domain.Entities;

public class UserWishlist
{
    [Key]
    public Guid Id { get; set; }
    public Guid UserId { get; set; }

    // Nav
    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;
    public ICollection<UserWishlistItem> UserWishlistItems { get; set; } = null!;
}
