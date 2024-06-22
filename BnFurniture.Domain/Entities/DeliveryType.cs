using System.ComponentModel.DataAnnotations;

namespace BnFurniture.Domain.Entities;

public class DeliveryType
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    // Nav
    public ICollection<OrderDetails> Details { get; set; } = new List<OrderDetails>();
}
