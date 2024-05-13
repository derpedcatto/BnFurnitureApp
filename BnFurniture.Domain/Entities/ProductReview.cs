using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BnFurniture.Domain.Entities;

public class ProductReview
{
    [Key]
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Guid AuthorId { get; set; }
    public int Rating { get; set; }
    public string Text { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    // Nav
    [ForeignKey(nameof(ProductId))]
    public Product Product { get; set; } = null!;

    [ForeignKey(nameof(AuthorId))]
    public User Author { get; set; } = null!;
}
