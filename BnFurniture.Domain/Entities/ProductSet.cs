using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BnFurniture.Domain.Entities;

public class ProductSet
{
    [Key]
    public Guid Id { get; set; }
    public Guid SetCategoryId { get; set; }
    public Guid AuthorId { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string? Summary { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int? Priority { get; set; }

    // Nav
    [ForeignKey(nameof(SetCategoryId))]
    public ProductSetCategory ProductSetCategory { get; set; } = null!;

    [ForeignKey(nameof(AuthorId))]
    public User Author { get; set; } = null!;

    public ICollection<ProductSetItem> ProductSetItems { get; set; } = null!;
}
