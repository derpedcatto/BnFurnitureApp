using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BnFurniture.Domain.Entities;

public class OrderItem
{
    [Key]
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Guid ArticleId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public int Discount { get; set; }

    //Nav
    [ForeignKey(nameof(OrderId))]
    public Order Order { get; set; } = null!;

    [ForeignKey(nameof(ArticleId))]
    public virtual ICollection<ProductArticle_OrderItem> ProductArticle { get; set; } = null!;

}
