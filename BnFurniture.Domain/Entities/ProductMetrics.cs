using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BnFurniture.Domain.Entities;

public class ProductMetrics
{
    [Key]
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public long Views { get; set; }
    public long Sales { get; set; }

    // Nav
    public Product Product { get; set; } = null!;
}
