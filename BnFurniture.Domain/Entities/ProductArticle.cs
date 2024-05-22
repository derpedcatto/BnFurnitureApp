using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BnFurniture.Domain.Entities;

public class ProductArticle
{
    [Key]
    public Guid Article { get; set; }
    public Guid ProductId { get; set; }
    public Guid AuthorId { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public decimal Price { get; set; } // Precision must be (19, 2)
    public int Discount { get; set; }
    public bool Active { get; set; }

    // Nav
    [ForeignKey(nameof(ProductId))]
    public Product Product { get; set; } = null!;

    [ForeignKey(nameof(AuthorId))]
    public User Author { get; set; } = null!;

    public virtual ICollection<ProductArticle_OrderItem> OrderItems { get; set; } = null!;
    public ICollection<ProductCharacteristicConfiguration> ProductCharacteristicConfigurations { get; set; } = null!;
    public ICollection<UserWishlistItem> UserWishlistItems { get; set; } = null!;
}