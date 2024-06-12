using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BnFurniture.Domain.Entities;

public class Order
{
    [Key]
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public int StatusId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }

    // Nav
    [ForeignKey(nameof(StatusId))]
    public OrderStatus Status { get; set; } = null!;

    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;
    public ICollection<OrderItem> OrderItem { get; set; } = null!;
}