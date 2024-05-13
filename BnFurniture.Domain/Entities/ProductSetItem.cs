using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BnFurniture.Domain.Entities;

public class ProductSetItem
{
    [Key]
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Guid ProductSetId { get; set; }

    // Nav
    [ForeignKey(nameof(ProductId))]
    public Product Product { get; set; } = null!;

    [ForeignKey(nameof(ProductSetId))]
    public ProductSet ProductSet { get; set; } = null!;
}
