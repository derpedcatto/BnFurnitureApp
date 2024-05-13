using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BnFurniture.Domain.Entities;

public class ProductSetCategory
{
    [Key]
    public Guid Id { get; set; }
    public Guid? ParentId { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public int? Priority { get; set; }

    // Nav
    [ForeignKey(nameof(ParentId))]
    public ProductSetCategory? ParentCategory { get; set; }
    public ICollection<ProductSet> ProductSets { get; set; } = null!;
}
