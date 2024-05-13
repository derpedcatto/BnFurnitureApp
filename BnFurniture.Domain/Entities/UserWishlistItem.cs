using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BnFurniture.Domain.Entities;

public class UserWishlistItem
{
    [Key]
    public Guid Id { get; set; }
    public Guid ProductArticleId { get; set; }
    public Guid UserWishlistId { get; set; }
    public DateTime AddedAt { get; set; }

    // Nav
    [ForeignKey(nameof(ProductArticleId))]
    public ProductArticle ProductArticle { get; set; } = null!;

    [ForeignKey(nameof(UserWishlistId))]
    public UserWishlist UserWishlist { get; set; } = null!;
}
