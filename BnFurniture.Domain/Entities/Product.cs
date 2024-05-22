using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BnFurniture.Domain.Entities;

public class Product
{
    [Key]
    public Guid Id { get; set; }
    public Guid ProductTypeId { get; set; }
    public Guid AuthorId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string? Summary { get; set; } 
    public string? Description { get; set; }
    public string? ProductDetails { get; set; }
    public int? Priority { get; set; }
    public bool Active { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    // Nav
    [ForeignKey(nameof(ProductTypeId))]
    public ProductType ProductType { get; set; } = null!;

    [ForeignKey(nameof(AuthorId))]
    public User Author { get; set; } = null!;

    public ProductMetrics Metrics { get; set; } = null!;

    public ICollection<ProductSetItem> ProductSetItems { get; set; } = null!;
    public ICollection<ProductReview> ProductReviews { get; set; } = null!;
    public ICollection<ProductArticle> ProductArticles { get; set; } = null!;
}
