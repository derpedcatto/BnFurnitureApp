using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BnFurniture.Domain.Entities;

public class OrderDetails
{
    [Key]
    public int Id { get; set; }
    public Guid OrderId { get; set; }
    public int PaymentTypeId { get; set; }
    public int DeliveryTypeId { get; set; }

    // Nav
    [ForeignKey(nameof(OrderId))]
    public Order Order { get; set; } = null!;

    [ForeignKey(nameof(PaymentTypeId))]
    public PaymentType PaymentType { get; set; } = null!;

    [ForeignKey(nameof(DeliveryTypeId))]
    public DeliveryType DeliveryType { get; set; } = null!;
}
