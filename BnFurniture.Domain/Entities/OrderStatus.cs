using System.ComponentModel.DataAnnotations;

namespace BnFurniture.Domain.Entities;

public class OrderStatus
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    // Nav
    public ICollection<Order> Orders { get; set; } = null!;
}
